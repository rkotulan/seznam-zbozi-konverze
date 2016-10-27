// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziOrderItem.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Entities
{
    public class ZboziOrderItem
    {
        /// <summary>
        ///     ID položky v e-shopu (ITEM_ID z feedu)
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        ///     Název položky, ideálně PRODUCTNAME z feedu
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     Počet zakoupených kusů
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        ///     Jednotková cena položky v Kč včetně DPH
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}