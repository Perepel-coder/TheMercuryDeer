using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tag ItemTag { get; set; }
        public Sprite Sprite { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public StatToChange StatToChange { get; set; }
        public float StatChangePercent { get; set; }
        public List<Category> Categories { get; set; }

        public Action UseItem { get; set; }

        public Type ItemType { get; set; }

        public Vector3 Scale { get; set; }
    }
}
