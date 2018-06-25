﻿using System;

namespace SavedItDwx.Models
{
    public class SavedItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}