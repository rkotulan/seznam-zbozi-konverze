// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziOrder.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Entities
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ZboziOrder
    {
        public ZboziOrder()
        {
            this.Cart = new List<ZboziOrderItem>();
        }

        /// <summary>
        ///     Obsah nákupního košíku
        /// </summary>
        public List<ZboziOrderItem> Cart { get; set; }

        /// <summary>
        ///     Datum, kdy má objednávka být předána dopravci nebo připravena k osobnímu odběru (je-li jich více termínů pro více
        ///     položek, vyberte nejzazší či takový, ve kterém půjde nejvíce zboží)
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        ///     Cena dopravy (bez ceny dobírky) v Kč včetně DPH
        /// </summary>
        public decimal DeliveryPrice { get; set; }

        /// <summary>
        ///     Způsob dopravy. Může být libovolný řetězec (např. Česká pošta, osobní odběr apod.). V administraci pak získáte
        ///     agregované statistiky jednodlivých způsobů dopravy.
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        ///     E-mail zákazníka. Může být využit pro ověření spokojenosti s nákupem a k žádosti o ohodnocení zakoupeného produktu.
        ///     Povinný pro získání přístupu k pokročilým statistikám, nezasílat v případě, kdy zákazník neudělil souhlas s jeho
        ///     poskytnutím.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Číslo/kód objednávky vygenerovaný vaším e-shopem. Je třeba aby se shodovalo u frontend i backend konverzního kódu,
        ///     aby mohly být údaje spojené.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        ///     Další náklady či slevy na objednávku, poplatek za dobírku, platbu kartou, instalace, množstevní sleva apod. Slevy
        ///     jsou uvedeny jako záporné číslo.
        /// </summary>
        public decimal OtherCosts { get; set; }

        /// <summary>
        ///     Způsob platby. Může být libovolný řetězec (např. kartou, hotovost apod.).
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        ///     Tajný klíč využívaný výhradně pro autorizaci požadavků z backendu, získáte také v administraci vaší provozovny,
        ///     případně na testovacím Sandboxu. Při prozrazení tohoto kódu si vygenerujte nový.
        /// </summary>
        [JsonProperty(PropertyName = "PRIVATE_KEY")]
        public string PrivateKey { get; set; }
        
        /// <summary>
        /// True pokud se jedná o testovací dostaz
        /// </summary>
        public bool Sandbox { get; set; }

        /// <summary>
        ///     Celková cena objednávky v Kč včetně DPH. Pokud není uvedena, bude vypočítána jako součet ceny nákupního košíku,
        ///     ceny dopravy a dalších nákladů na objednávku
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}