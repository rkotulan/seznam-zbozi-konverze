// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziKonverzeServiceTest.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Tests
{
    using System;

    using KSystem.Seznam.KonverzeZbozi.Entities;
    using KSystem.Seznam.KonverzeZbozi.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ZboziKonverzeServiceTest
    {
        [TestMethod]
        public void SendDataToServer()
        {
            // odeslání je možné ověžit na https://sandbox.zbozi.cz/ 
            var tajnyKlic = "b94161aa8e646800952a73c6a00cc707";
            var provozovnaId = "58c831510f64b07b405334f64a5d0a1f";

            var order = new ZboziOrder();
            order.Sandbox = true; //// true pro testovací režim

            order.DeliveryDate = DateTime.Today.AddDays(2);
            order.DeliveryPrice = 15;
            order.DeliveryType = "Česká pošta";
            order.Email = "agent.smith@matrix.com";
            order.OrderId = "1926321340";
            order.OtherCosts = 0;
            order.PaymentType = "Kartou";
            order.PrivateKey = tajnyKlic;
            order.TotalPrice = 450;
            order.Cart.Add(
                new ZboziOrderItem { ItemId = "10005", ProductName = "Kosile", Quantity = 2, UnitPrice = 217.5M });

            var service = new ZboziKonverzeService(provozovnaId);
            var response = service.Send(order);

            Assert.AreEqual(200, response.Status);
        }
    }
}