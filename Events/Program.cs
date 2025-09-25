namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var stock = new Stock("Amazon");
            stock.Price = 100;

            stock.OnPriceChanged += Stock_OnPriceChanged; // Subscribe to the event
            stock.ChangeStockPriceBy(0.05);
            stock.ChangeStockPriceBy(-0.02);
            stock.ChangeStockPriceBy(0.00);

            stock.OnPriceChanged -= Stock_OnPriceChanged; // Unsubscribe to the event
            stock.ChangeStockPriceBy(0.07);
            stock.ChangeStockPriceBy(-0.01);
            stock.ChangeStockPriceBy(0.00);

            Console.ReadKey();
        }

        static void Stock_OnPriceChanged(Stock stock, double oldPrice)
        {
            string result;

            if (stock.Price > oldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                result = "Up";
            }
            else if (stock.Price < oldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                result = "Down";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                result = "Not Changed";
            }

            Console.WriteLine($"{stock.Name}: ${stock.Price} - {result}");
        }
    }

    public delegate void StockPriceChangeHandler(Stock stock, double oldPrice);

    public class Stock
    {
        private string name;
        private double price;

        public string Name => this.name;
        public double Price { get => this.price; set => this.price = value; }

        public Stock(string stockName)
        {
            this.name = stockName;
        }

        public event StockPriceChangeHandler OnPriceChanged; // Declare the event

        public void ChangeStockPriceBy(double percent)
        {
            double oldPrice = this.price;

            this.price += (this.price * percent);

            if (OnPriceChanged != null) // Make sure there is a subscriber
            {
                OnPriceChanged(this, oldPrice); // Fire the event
            }
        }
    }
}
