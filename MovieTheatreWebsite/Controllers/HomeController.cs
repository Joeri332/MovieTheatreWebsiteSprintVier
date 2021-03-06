using Microsoft.AspNetCore.Mvc;
using MovieTheatreWebsite.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using MovieTheatreModels.Dto;
using MovieTheatreModels.Enums;
using static MovieTheatreWebsite.Statics.Statics;
using Stripe.Checkout;

namespace MovieTheatreWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieTheatreDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, MovieTheatreDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //Order moviethreatreroom based on datetime, use this ordering to pick movies 1 by 1, skipping ones you already added
            var movieTheatreRoomsList =
                _context.MovieTheatreRooms
                    .Include(x => x.TheatreRoom)
                    .Include(x=> x.Reservations).ThenInclude(x => x.ReservationChairNr)
                    .Where(x => x.DateTime > DateTime.Now && x.DateTime < DateTime.Now.AddDays(7)) //Only show movies between today and next week
                    .OrderBy(x => x.DateTime).ToList();


            var movieList = new List<Movie>();
            foreach (var movieTheatreRoom in movieTheatreRoomsList)
            {
                var movie = _context.Movies.ToList().Find(x => x.MovieId == movieTheatreRoom.MovieId);
                //Movies can be part of multiple moviethreatrerooms, only add the first occurence
                if (movie != null && !movieList.Contains(movie))
                {
                    movieList.Add(movie);
                }

            }

            ViewData["MovieTheatreRooms"] = movieTheatreRoomsList;
            return View(movieList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //function for creating a list for free chairs
        public Reservation DetailsInformation(int movieTheatreRoomId, int? priceIdSpecial, int? priceId, string? voucherCode, int? chairCount, bool? automaticMode, string? checkBoxChairs, ReservationDto? reservation = null)
        {
            Reservation reservationObj = new Reservation();
            reservationObj.MovieTheatreRoomId = movieTheatreRoomId;
            reservationObj.MovieTheatreRoom = _context.MovieTheatreRooms.Include(u => u.Movie)
                .Include(u => u.TheatreRoom)
                .First(u => u.MovieTheatreRoomId == movieTheatreRoomId);

            PriceCategory? priceType;
            switch (reservationObj.MovieTheatreRoom.TheatreRoom.TheatreRoomCategory)
            {
                case TheatreRoomCategory.Regular:
                    if (reservationObj.MovieTheatreRoom.Movie.Duration > MovieLengthLong)
                        priceType = PriceCategory.RegularLong;
                    else
                        priceType = PriceCategory.RegularShort;
                    break;
                case TheatreRoomCategory.ThreeD:
                    if (reservationObj.MovieTheatreRoom.Movie.Duration > MovieLengthLong)
                        priceType = PriceCategory.Movie3DLong;
                    else
                        priceType = PriceCategory.Movie3DShort;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //creating List from allready taken chairs
            var takenChairs = _context.ReservationChairNr
                .Include(x => x.Reservation)
                .Where(x => x.Reservation.MovieTheatreRoom.MovieTheatreRoomId == movieTheatreRoomId)
                .Select(x => x.ChairNr)
                .ToList();

            //takenChairs is all the red chairs
            ViewData["takenChairs"] = takenChairs;
                              //CheckboxChairs is url checkbox value
            var chosenChairs = checkBoxChairs?.Split(',').Select(int.Parse).Except(takenChairs).ToList() ?? new List<int>();

            var selectedChairs = takenChairs.ToList();
            if (checkBoxChairs != null)
            {
                selectedChairs.AddRange(checkBoxChairs.Split(',').Select(int.Parse));
                selectedChairs = selectedChairs.Distinct().ToList();
            }

            //SelectedChairs is all the red and green chairs together
            ViewData["SelectedChairs"] = selectedChairs;

            var price = _context.Prices.FirstOrDefault(x => x.PriceId == priceId) ?? _context.Prices.First(x => x.PriceType == priceType);
            var voucher = _context.VoucherCodes.Include(x => x.Price).FirstOrDefault(x => x.Code == voucherCode && x.PriceId == priceId);
            var priceSpecial = _context.Prices.FirstOrDefault(x => x.PriceId == priceIdSpecial) ?? null;


            // VoucherCode
            reservationObj.Prices = voucher != null ? voucher.Price : price;

            ViewData["Voucher"] = voucher;

            ViewData["VoucherCode"] = voucherCode;

            //Filter types of prices dependant on movie Movie DurationType on both prices and movies
            ViewData["PriceSelect"] = new SelectList(_context.Prices.Where(x => x.PriceType == priceType || x.PriceType == PriceCategory.Code), "PriceId", "Name");
            if (voucher != null)
            {
                TempData["success"] = "Voucher Code accepted!";
                if (priceSpecial == null)
                {
                    ViewData["TotalPrice"] = (price.Amount) * (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count);
                }
                else
                {
                    ViewData["TotalPrice"] = (price.Amount + priceSpecial.Amount) * (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count);
                }
                ViewData["ChairCount"] = 1;
            }
            else
            {
                if (reservationObj.Prices.PriceId == 17)
                {
                    ViewData["TotalPrice"] = _context.Prices.FirstOrDefault(x => x.PriceType == priceType).Amount;
                    ViewData["ChairCount"] = 1;
                }
                else
                {
                    if (priceSpecial == null)
                    {
                        ViewData["TotalPrice"] = (price.Amount) * (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count);
                    }
                    else
                    {
                        ViewData["TotalPrice"] = (price.Amount + priceSpecial.Amount) * (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count);
                    }
                    ViewData["ChairCount"] = reservation?.ChairCount > 0 ? reservation.ChairCount : chairCount ?? 1;
                }
            }

            ViewData["SpecialArrangements"] = new SelectList(_context.Prices.Where(x => x.PriceType == PriceCategory.SpecialArrangements), "PriceId", "Name");

            ViewData["TheatreRoomChairNrSelect"] = new SelectList(_context.TheatreRooms, "TheatreRoomId", "ChairCount");
            return reservationObj;
        }

        public IActionResult Details(int movieTheatreRoomId, int? priceIdSpecial, int? priceId, string? voucherCode, int? chairCount, bool? automaticMode, string? checkBoxChairsValues)
        {
            if (!automaticMode.HasValue || automaticMode.Value)
            {
                ViewData["hideManual"] = "style=display:none";
                ViewData["hiddenAutomatic"] = "True";
            }
            else
            {
                ViewData["hideAutomatic"] = "style=display:none";
                ViewData["hiddenAutomatic"] = "False";
            }

            return View(DetailsInformation(movieTheatreRoomId, priceIdSpecial, priceId, voucherCode, chairCount, automaticMode, checkBoxChairsValues));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int movieTheatreRoomId, int? priceIdSpecial, int? priceId, string? voucherCode, int? chairCount, bool? automaticMode, string? checkBoxChairsValues, [Bind("UserId,MovieTheatreRoomId,PriceId")] ReservationDto reservation)
        {
            if (!automaticMode.HasValue || automaticMode.Value)
            {
                ViewData["hideManual"] = "style=display:none;";
                ViewData["hiddenAutomatic"] = "True";
            }
            else
            {
                ViewData["hideAutomatic"] = "style=display:none;";
                ViewData["hiddenAutomatic"] = "False";
            }

            var chairCountSuccess = Request.Form.TryGetValue("MovieTheatreRoom.TheatreRoom.ChairCount", out var chairCountStringValue);
            chairCountSuccess = int.TryParse(chairCountStringValue, out var reservationChairCount) && chairCountSuccess;
            reservation.ChairCount = reservationChairCount;
            reservation.PriceIdSpecial = priceIdSpecial;
            

            var reservationObj = DetailsInformation(movieTheatreRoomId, priceIdSpecial, priceId, voucherCode, chairCount, automaticMode, checkBoxChairsValues, reservation);

            if (!chairCountSuccess)
                return View(reservationObj);

            var chosenChairs = checkBoxChairsValues?.Split(',').Select(int.Parse).Except((ViewData["takenChairs"] as List<int>)!).ToList();

            if (automaticMode.HasValue && !automaticMode.Value && chosenChairs.Count == 0)
                return View(reservationObj);

            //creating list from MovieTheatreRoom ChairCount
            var chairCountTotalList = new List<int>();
            for (int i = 1; i <= reservationObj.MovieTheatreRoom.TheatreRoom.ChairCount; i++)
            {
                chairCountTotalList.Add(i);
            }

            //Removing the entrys where the chairs are allready taken
            var availableChairNrList = chairCountTotalList.Except((ViewData["takenChairs"] as List<int>)!).ToList();


            //checking if the number of chairs added with the reservation doesnt exceed the amount of chairs
            var reservationChairCountSuccess = true;
            if (!automaticMode.HasValue || automaticMode.Value)
            {
                if (availableChairNrList.Count() < reservation.ChairCount)
                {
                    reservationChairCountSuccess = false;
                }
            }
            else
            {
                if (availableChairNrList.Count() < chosenChairs.Count())
                {
                    reservationChairCountSuccess = false;
                }
            }
            if (!reservationChairCountSuccess)
            {

                TempData["error"] = "NO MORE SPACE. Only " + availableChairNrList.Count + " more seats available";
            }

            if (ModelState.IsValid && chairCountSuccess && reservationChairCountSuccess)
            {
                
                var reservationDb = _context.Reservations.Add(reservation.ToDb());
                await _context.SaveChangesAsync();

                if (!automaticMode.HasValue || automaticMode.Value)
                {
                    for (int i = 0; i < reservation.ChairCount; i++)
                    {
                        _context.ReservationChairNr.Add(new ReservationChairNr(reservationDb.Entity.ReservationId,
                            availableChairNrList[i]));
                    }
                }
                else
                {
                    foreach (int item in chosenChairs)
                    {
                        _context.ReservationChairNr.Add(new ReservationChairNr(reservationDb.Entity.ReservationId,
                            item));
                    }
                }

                await _context.SaveChangesAsync();

                //stripe settings
                if (reservationObj.Prices.Amount != 0 || priceIdSpecial != null)
                {
                    var priceSpecial = _context.Prices.FirstOrDefault(x => x.PriceId == priceIdSpecial);
                    var domain = "https://localhost:44330/";
                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                    {
                        "card",
                        "ideal",
                    },
                        LineItems = new List<SessionLineItemOptions>(),
                        Mode = "payment",
                        SuccessUrl = domain +
                                     $"Home/ReservationConfirmation?ReservationGuid={reservationDb.Entity.ReservationGuid}",
                        CancelUrl = domain + $"Home/ReservationCancelConfirmation?ReservationGuid={reservationDb.Entity.ReservationGuid}",
                    };

                    if (priceSpecial == null)
                    {
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(reservationObj.Prices.Amount * 100), //20.00 -> 2000,
                                Currency = "eur",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = reservationObj.MovieTheatreRoom.Movie.Name,
                                },
                            },
                            Quantity = (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count),
                        };

                        options.LineItems.Add(sessionLineItem);
                    }
                    else
                    {
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)((reservationObj.Prices.Amount + priceSpecial.Amount) * 100), //20.00 -> 2000,
                                Currency = "eur",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = reservationObj.MovieTheatreRoom.Movie.Name,
                                },
                            },
                            Quantity = (!automaticMode.HasValue || automaticMode.Value ? chairCount ?? 1 : chosenChairs.Count),
                        };

                        options.LineItems.Add(sessionLineItem);
                    }
                    var service = new SessionService();
                    Session session = service.Create(options);
                    Response.Headers.Add("Location", session.Url);
                    return new StatusCodeResult(303);
                }
                else
                {
                    var domain = "https://localhost:44330/";
                    var SuccessUrl = domain + $"Home/ReservationConfirmation?ReservationGuid={reservationDb.Entity.ReservationGuid}";
                    return Redirect(SuccessUrl);
                }
            }
            return View(reservationObj);
        }


        [HttpGet]
        public IActionResult ReservationConfirmation(Guid reservationGuid)
        {
            var reservation = _context.Reservations
                .Include(x => x.MovieTheatreRoom)
                .Include(x => x.MovieTheatreRoom.TheatreRoom)
                .Include(x => x.ReservationChairNr)
                .FirstOrDefault(x => x.ReservationGuid == reservationGuid);

            return View(reservation);
        }

        [HttpGet]
        public IActionResult ReservationCancelConfirmation(Guid reservationGuid)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.ReservationGuid == reservationGuid);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SecretMovie()
        {

            var movieTheatreRoomsList =
                _context.MovieTheatreRooms
                .Include(x => x.TheatreRoom)
                .Where(x => x.DateTime > DateTime.Now && x.DateTime < DateTime.Now.AddDays(7))
                .ToList();

            var random = new Random();

            int index = random.Next(1, movieTheatreRoomsList.Count);

            return RedirectToAction("Details", new { movieTheatreRoomId = index });
        }

        //---------------------------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin, Manager, Cashier")]
        public Reservation ReservationSeatChangeInformation(int movieTheatreRoomId, int? reservationGuid)
        {
            Reservation reservationObj = new Reservation();
            reservationObj.MovieTheatreRoomId = movieTheatreRoomId;
            reservationObj.MovieTheatreRoom = _context.MovieTheatreRooms.Include(u => u.Movie)
                .Include(u => u.TheatreRoom)
                .First(u => u.MovieTheatreRoomId == movieTheatreRoomId);


            ViewData["SelectReservation"] =new SelectList(_context.Reservations
                .Include(x => x.MovieTheatreRoom)
                .Where(t => t.MovieTheatreRoomId == movieTheatreRoomId)
                .ToList(), "ReservationId", "ReservationGuid", reservationGuid);

            //Get the selected reservation if not null
            var reservation = _context.Reservations.FirstOrDefault(x => x.ReservationId == reservationGuid);

            //creating List from allready taken chairs
            var takenChairsAllReservations = _context.ReservationChairNr
                .Include(x => x.Reservation)
                .Where(x => x.Reservation.MovieTheatreRoom.MovieTheatreRoomId == movieTheatreRoomId)
                .ToList();

            //Filtering all already taken chairs down to the taken chairs from the selected reservation
            var takenChairsFromSelectedReservations = takenChairsAllReservations.Where(x => x.ReservationId == reservation?.ReservationId).Select(x => x.ChairNr).ToList();
            ViewData["TakenChairsFromSelectedReservationGuid"] = takenChairsFromSelectedReservations.Count;

            var takenChairs = takenChairsAllReservations.Select(x => x.ChairNr).ToList();
            var selectedChairs = takenChairs.ToList();

            //The red chairs
            ViewData["takenChairs"] = takenChairs.Except(takenChairsFromSelectedReservations).ToList();

            //All chairs
            ViewData["SelectedChairs"] = selectedChairs;

            return reservationObj;
        }

        // Getting the attrubutes from the url including the state of the dropdown
        [Authorize(Roles = "Admin, Manager, Cashier")]
        public IActionResult ReservationSeatChange(int movieTheatreRoomId, int? reservationGuid)
        {
            return View(ReservationSeatChangeInformation(movieTheatreRoomId, reservationGuid));
        }

        [Authorize(Roles = "Admin, Manager, Cashier")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservationSeatChange(int movieTheatreRoomId, int? reservationGuid, [Bind("UserId,MovieTheatreRoomId,PriceId")] ReservationDto reservation)
        {
            var reservationObj = ReservationSeatChangeInformation(movieTheatreRoomId, reservationGuid);

            var success = Request.Form.TryGetValue("checkBoxChairs", out var checkBoxChairsStringValue);

            success = Request.Form.TryGetValue("ReservationGuid", out var stringValue) && success;
            success = int.TryParse(stringValue, out var ChairNrInt) && success;

            if (!success)
                return View(reservationObj);

            var listOfChainrs = _context.ReservationChairNr
                .Include(x => x.Reservation)
                .Where(x => x.ReservationId == ChairNrInt)
                .ToList();

            var chosenChairs = checkBoxChairsStringValue.ToString().Split(',').Select(int.Parse).Except((ViewData["takenChairs"] as List<int>)!).ToList();
            
            if (chosenChairs.Count == (ViewData["TakenChairsFromSelectedReservationGuid"] as int? ))
            {
                foreach (var chairNr in listOfChainrs)
                {
                    _context.Remove(chairNr);
                    await _context.SaveChangesAsync();
                }

                foreach (int item in chosenChairs)
                {
                    _context.ReservationChairNr.Add(new ReservationChairNr(ChairNrInt, item));
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                TempData["error"] = "You have selected " + (chosenChairs.Count - (ViewData["takenchairs1"] as int?) ) +
                                    " More than the original reservation";
            }


            return RedirectToAction("ReservationSeatChange", new { movieTheatreRoomId = movieTheatreRoomId });

        }
    }
}