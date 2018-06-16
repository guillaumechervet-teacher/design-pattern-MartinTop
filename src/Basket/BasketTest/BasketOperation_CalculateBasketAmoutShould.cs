﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Basket;
using Basket.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BasketTest
{ 
    [TestClass]
    public partial class BasketOperationCalculateBasketAmoutShould
    {
        public class BasketTest
        {
            public List<BasketLineArticle> BasketLineArticles { get; set; }
            public int ExpectedPrice { get; set; }
        }
        private static IEnumerable<object[]> Baskets
        {
            get
            {
                return new[]
                { new object[] {
                        new BasketTest(){ BasketLineArticles = new List<BasketLineArticle>
                            {
                                new BasketLineArticle {Id = "1", Number = 12, Label = "Banana"},
                                new BasketLineArticle {Id = "2", Number = 1, Label = "Fridge electrolux"},
                                new BasketLineArticle {Id = "3", Number = 4, Label = "Chair"}
                            },
                            ExpectedPrice = 84868}
                    },
                    new object[] {
                        new BasketTest(){ BasketLineArticles = new List<BasketLineArticle>
                            {
                                new BasketLineArticle {Id = "1", Number = 20, Label = "Banana"},
                                new BasketLineArticle {Id = "3", Number = 6, Label = "Chair"}
                            },
                            ExpectedPrice = 37520}
                    },
                    new object[] {
                        new BasketTest(){ BasketLineArticles = new List<BasketLineArticle>
                            {
                                new BasketLineArticle {Id = "4", Number = 2, Label = "Grumlys"},
                            },
                            ExpectedPrice = 8640}
                    },
                };

            }
        }
        
        [TestMethod]
        [DynamicData("Baskets")]
        public void ReturnCorrectAmoutGivenBasket(BasketTest basketTest)
        {
            var basKetService = new BasketService(new ArticleDatabaseMock());
            // var basKetService = new BasketService(new ArticleDatabaseJson());
            var basketOperation = new BasketOperation(basKetService);
            var amountTotal = basketOperation.CalculateAmout(basketTest.BasketLineArticles);
            Assert.AreEqual(amountTotal, basketTest.ExpectedPrice);
        }
        
        [TestMethod]
        public void ReturnCorrectAmoutGivenBasketLine()
        {
            var basketLineArticle = new BasketLineArticle {Id = "4", Number = 2,
                Label = "Grumy"};
            var basKetService = new BasketService(new ArticleDatabaseMock());
            var basketOperation = new BasketOperation(basKetService);
            var amountTotal = basketOperation.CalculateAmout(basketLineArticle);
            Assert.AreEqual(amountTotal, 8640);
        }
    }
}