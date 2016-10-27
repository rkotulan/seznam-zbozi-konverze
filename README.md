# Pokročilé měření konverzí Zboží.cz
C# kód pro pokročilé měření konverzí na Zboží.cz

Orginální kód pro PHP je s popisem zde https://github.com/seznam/zbozi-konverze

## Jak začít
Příkld jak použít knihovnu pro odeslání objednávky s položkou je zde

    // odeslání je možné ověřit na https://sandbox.zbozi.cz/ 
    var tajnyKlic = "b94161aa8e646800952a73c6a00cc707";
    var provozovnaId = "58c831510f64b07b405334f64a5d0a1f";

    var order = new ZboziOrder();
    order.Sandbox = true; //// true pro testovací režim

    order.DeliveryDate = DateTime.Today.AddDays(2);
    order.DeliveryPrice = 15;
    order.DeliveryType = "Česká pošta";
    order.Email = "agent.smith@matrix.com";
    order.OrderId = "1926321343";
    order.OtherCosts = 0;
    order.PaymentType = "Kartou";
    order.PrivateKey = tajnyKlic;
    order.TotalPrice = 450;
    order.Cart.Add(
        new ZboziOrderItem { ItemId = "10005", ProductName = "Kosile", Quantity = 2, UnitPrice = 217.5M });

    var service = new ZboziKonverzeService(provozovnaId);
    var response = await service.SendAsync(order);
        
## NuGet
Balíček **Měření konverzí na Zboží.cz pro C#** nainstalujete pomocí [NuGet](https://www.nuget.org)
  
    PM> Install-Package KonverzeZbozi
