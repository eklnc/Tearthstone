using System.Collections.Generic;
using Tearthstone.Service.Models.Base;

namespace TearthStone.Service.Models
{
    public class DeckModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<CardBase> Cards { get; set; }
    }
}
