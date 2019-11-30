﻿using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class RupeeUpgradeItem : Item
    {
        private int _price;
        public RupeeUpgradeItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite
       
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupItem();
                    player.Inventory.MaxRupeeCount = player.Inventory.MaxRupeeCount *2;
                }
                else {
                    Used = false;
                }
            }
            else {
            SoundEffectManager.Instance.PlayPickupItem();
            }
         return new NoOp();       
        }
    }
}
