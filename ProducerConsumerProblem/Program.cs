using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerProblem
{
    class Program
    {
        static Queue<Product> products = new Queue<Product>();
        static Random r = new Random(10);
        static int p = r.Next();
        static int c = r.Next();

        static void Producer()
        {
                if (products.Count == 0)
                {
                    for (int i = 0; i < p; i++)
                    {
                        Product product = new Product("Product", i);
                        lock (products)
                        {
                            products.Enqueue(product);
                            Monitor.Pulse(products);
                            Console.WriteLine("Producer produciendo productos..\n");
                            Thread.Sleep(250);


                        }
                    }
                }
        }

        static void Consumer()
        {
            for (int i = 0; i < c; i++)
            {
                Product product = new Product();
                lock (products)
                {
                    while (products.Count == 0)
                    {
                        Console.WriteLine("Consumer esperando a que haya productos!\n");
                        Monitor.Wait(products);
                        Thread.Sleep(250);


                    }

                    product = products.Dequeue();
                    Console.WriteLine("Consumer consumiendo..\n");
                }
            }
        }

        public class Product
        {
            string name;
            int productNumber;

            public Product(string name, int productNumber)
            {
                this.name = name;
                this.productNumber = productNumber;
            }

            public Product() { }
        }

        static void Main(string[] args)
        {

            Thread producerThread = new Thread(Producer);
            Thread consumerThread = new Thread(Consumer);
            producerThread.Start();
            Console.WriteLine("***Producer empezando***");
            consumerThread.Start();
            Console.WriteLine("***Consumer empezando***");
            Console.ReadLine();
            producerThread.Join();
            consumerThread.Join();

        }
    }
}
