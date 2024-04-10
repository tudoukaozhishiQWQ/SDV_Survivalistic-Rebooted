﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using Survivalistic_Rebooted.Framework.Bars;

namespace Survivalistic_Rebooted.Framework.Common
{
    public static class Textures
    {
        public static Texture2D HungerSprite, ThirstSprite;

        public static Texture2D BuffSprites;

        private static Texture2D _hungerFiller;

        private static Texture2D _thirstFiller;

        public static Texture2D HungerFiller
        {
            get
            {
                Color color = BarsInformations.GetOffsetHungerColor();
                _hungerFiller.SetData(new[] { color });

                return _hungerFiller;
            }

            set
            {
                _hungerFiller = value;
            }
        }

        public static Texture2D ThirstFiller
        {
            get
            {
                Color color = BarsInformations.GetOffsetThirstyColor();
                _thirstFiller.SetData(new[] { color });

                return _thirstFiller;
            }

            set
            {
                _thirstFiller = value;
            }
        }

        public static void LoadTextures()
        {
            HungerSprite = ModEntry.Instance.Helper.ModContent.Load<Texture2D>("assets/Bars/Hunger_Sprite.png");
            ThirstSprite = ModEntry.Instance.Helper.ModContent.Load<Texture2D>("assets/Bars/Thirst_Sprite.png");

            BuffSprites = ModEntry.Instance.Helper.GameContent.Load<Texture2D>("TileSheets/BuffsIcons");

            _hungerFiller = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            _thirstFiller = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
        }
    }
}
