﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napilnik2
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            
            warehouse.Show(); //
            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            //cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе
            Console.WriteLine();
            warehouse.Show();

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары

            Console.ReadLine();
        }
    }

    public class Good
    {
        private string _label;

        public Good(string label)
        {
            _label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public string Label => _label;
    }

    public class Warehouse
    {
        private readonly Dictionary<Good, int> _goodsStock;

        public Warehouse()
        {
            _goodsStock = new Dictionary<Good, int>();
        }
        public Dictionary<Good, int> GoodsStock => _goodsStock;

        public void Delive(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (_goodsStock.ContainsKey(good))
                _goodsStock[good] += count;
            else
                _goodsStock.Add(good, count);
        }

        public void Remove(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (_goodsStock[good] - count < 0)
                throw new ArgumentOutOfRangeException(nameof(good));

            _goodsStock[good] -= count;
        }


        public void Show()
        {
            if (_goodsStock.Count == 0)
                Console.WriteLine("На складе нет товара");
            else
            {
                Console.WriteLine("На складе:");

                foreach (var good in _goodsStock)
                {
                    Console.WriteLine($"{good.Key.Label} , количество - {good.Value}");
                }
            }
        }
    }

    public class Shop
    {
        private Warehouse _warehouse; 

        public Shop(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            _warehouse = warehouse;
        }

        public Warehouse Warehouse => _warehouse;

        public Cart Cart()
        {
            return new Cart(new Shop(_warehouse));
        }
    }

    public class Cart
    {
        private readonly Dictionary<Good, int> _goodsStock;
        private Shop _shop;

        public Cart(Shop shop)
        {
            _goodsStock = new Dictionary<Good, int>();
            _shop = shop;
        }

        public void Add(Good good, int count) //Дубляж получется?
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (_goodsStock.ContainsKey(good))
            {
                _goodsStock[good] += count;
                _shop.Warehouse.Remove(good, count);// Проверить на раочесть
                return;
            }

            _goodsStock.Add(good, count);
            _shop.Warehouse.Remove(good, count);//  Проверить на раочесть
        }

        public Order Order()
        {
            StringBuilder paylinkBuilder = new StringBuilder();

            foreach (var good in _goodsStock.Keys)
            {
                paylinkBuilder.Append(good.Label);
                paylinkBuilder.Append(" ");
            }

            return new Order(paylinkBuilder.ToString());
        }
    }

    public struct Order
    {
        public Order(string paylink)
        {
            Paylink = paylink;
        }

        public string Paylink { get; }
    }
}