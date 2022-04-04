using System;

namespace Lesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var paymentForm = orderForm.ShowForm();

            paymentHandler.ShowPaymentResult(paymentForm);
        }
    }

    public class OrderForm
    {
        public IPaySystem ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");
            Console.WriteLine("Какой системой вы хотите совершить оплату?");

            string systemId = Console.ReadLine().ToLower();

            switch (systemId)
            {
                case "qiwi": return new Qiwi();
                case "webmoney": return new Webmoney();
                case "card": return new Card();

                default: return new NotIdentifiend();
            }
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaySystem systemId)
        {
            if (systemId == null)
                throw new ArgumentNullException(nameof(systemId));

            Console.WriteLine($"Вы оплатили с помощью {systemId.Name}");

            systemId.Verification();

            if (systemId.SuccessfulPay == true)
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Ошибка оплаты");
        }
    }

    public interface IPaySystem
    {
        bool SuccessfulPay { get; }

        string Name { get; }

        void Verification();
    }

    public class Qiwi : IPaySystem
    {
        public bool SuccessfulPay { get; private set; } = true;

        public string Name => "Qiwi";

        public void Verification()
        {
            Console.WriteLine("Проверка платежа через QIWI...");
        }
    }

    public class Webmoney : IPaySystem
    {
        public bool SuccessfulPay { get; private set; } = true;

        public string Name => "WebMoney";

        public void Verification()
        {
            Console.WriteLine("Проверка платежа через WebMoney...");
        }
    }

    public class Card : IPaySystem
    {
        public bool SuccessfulPay { get; private set; } = true;

        public string Name => "Card";

        public void Verification()
        {
            Console.WriteLine("Проверка платежа через Card...");
        }
    }

    public class NotIdentifiend : IPaySystem
    {
        public bool SuccessfulPay { get; private set; } = false;

        public string Name => "Не идентифицирован";

        public void Verification()
        {
            Console.WriteLine("Не найдена система оплаты");
        }
    }
}
   
