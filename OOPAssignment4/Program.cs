#region part1
//Q1
//Static Binding (Compile-time)
//Happens with method overloading, depends on reference type

//Dynamic Binding (Run-time)
//happens with method overriding using inheritance, depends on the actual object type

//Q2
// Method Overloading:
//Same method name, different parameters (number/type)
//Happens in the same class
//Resolved at compile time

//Method Overriding:
//Reimplementing a base class method in a derived class
//same method signature
//Resolved at runtime

//Q3
//virtual  -> used in base class to allow a method to be overridden
//override -> used in derived class to provide new implementation
#endregion

#region part2
using System;

namespace MovieTicketBookingSystem
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; protected set; }

        public decimal PriceAfterTax => Price * 1.14m;

        public Ticket(int id, string movieName)
        {
            TicketId = id;
            MovieName = movieName;
        }
        //1
        public void SetPrice(decimal price)
        {
            Console.WriteLine($"Setting price directly: {price}");
            Price = price;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
            Console.WriteLine($"Setting price with multiplier: {basePrice} x {multiplier} = {Price}");
        }

        public virtual void PrintTicket()
        {
            Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | After Tax: {PriceAfterTax:F2} EGP");
        }
    }

    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(int id, string movieName, string seat)
            : base(id, movieName)
        {
            SeatNumber = seat;
        }

        //2
        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"  Seat: {SeatNumber}");
        }
    }
    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; }

        public VIPTicket(int id, string movieName, bool lounge, decimal fee)
            : base(id, movieName)
        {
            LoungeAccess = lounge;
            ServiceFee = fee;
        }
        //2
        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"  Lounge: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP");
        }
    }

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(int id, string movieName, bool is3D)
            : base(id, movieName)
        {
            Is3D = is3D;
        }
        //2
        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"  IMAX 3D: {(Is3D ? "Yes" : "No")}");
        }
    }

    //3
    public class Cinema
    {
        private Ticket[] tickets = new Ticket[10];
        private int count = 0;

        public void OpenCinema()
        {
            Console.WriteLine("========== Cinema Opened ==========");
            Console.WriteLine("Projector started.\n");
        }

        public void CloseCinema()
        {
            Console.WriteLine("\nProjector stopped.");
            Console.WriteLine("========== Cinema Closed ==========");
        }

        public void AddTicket(Ticket ticket)
        {
            if (count < tickets.Length)
            {
                tickets[count++] = ticket;
            }
        }
        //3
        public void PrintAllTickets()
        {
            Console.WriteLine("\n========== All Tickets ==========");
            for (int i = 0; i < count; i++)
            {
                tickets[i].PrintTicket(); 
            }
        }

        //4
        public static void ProcessTicket(Ticket t)
        {
            Console.WriteLine("\n========== Process Single Ticket ==========");
            t.PrintTicket();
        }
    }

    //5
    class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema();
            cinema.OpenCinema();

            StandardTicket t1 = new StandardTicket(1, "Inception", "A-5");
            VIPTicket t2 = new VIPTicket(2, "Avengers", true, 50);
            IMAXTicket t3 = new IMAXTicket(3, "Dune", false);

            Console.WriteLine("========== SetPrice Test ==========");
            t1.SetPrice(150);
            t1.SetPrice(100, 1.5m);

            t2.SetPrice(200);
            t3.SetPrice(180);

            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            cinema.PrintAllTickets();

            Cinema.ProcessTicket(t2);

            cinema.CloseCinema();
        }
    }
}
#endregion
