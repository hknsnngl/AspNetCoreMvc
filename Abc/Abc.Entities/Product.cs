using Abc.Core.Entities;               //Entities de Core referans verdil IEntity için
using System.ComponentModel.DataAnnotations;

namespace Abc.Entities
{                                       //IEntity Core da tanımlanmalı
    public class Product : IEntity      //IEntity : bu sınıf VT nesnesi oldugu söyler.
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public short UnitsInStock { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
