﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Tiled
{
    public class Layer
    {
        // the raw level data
        public int[] Data { get; set; }

        // reference to the level
        public Level Level;

        // the texture from the tiles will
        // be rendered from
        public Texture2D Texture;

        // may be used to offset layers
        public int X { get; set; }
        public int Y { get; set; }
        
        // size of the layer in tile count
        public int Width { get; set; }
        public int Height { get; set; }

        // name of the layer
        public string Name { get; set; }

        public int Opacity { get; set; }
        public string Type { get; set; }

        // if it is visible or not
        public bool Visible { get; set; }

        // the level data represented in an two-dimensional array
        // may not be the best option.
        // maybe just use the raw data instead.
        public Tile[,] TileMap;

        public Layer(Level level, Texture2D texture, int width, int height)
        {
            this.Level = level;
            this.Texture = texture;

            this.Width = width;
            this.Height = height;

            this.TileMap = new Tile[height, width];

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            for(int y = 0; y < this.TileMap.GetLength(0); y++)
            {

                for(int x = 0; x < this.TileMap.GetLength(1); x++)
                {

                    this.TileMap[y, x]?.Draw(gameTime, spriteBatch);

                }

            }

        }

        public void MakeTiles()
        {

            for(int i = 0; i < this.Data.Length; i++)
            {

                if (this.Data[i] != 0)
                {

                    // Column = TileID mod Rows
                    // Row = TileID div Rows

                    int x = i % this.Width;
                    int y = i / this.Width;

                    this.TileMap[y, x] = new Tile(
                        null,
                        this,
                        this.Texture,
                        new Vector2(
                            this.Data[i] % (this.Texture.Width / this.Level.TileWidth) - 1,
                            this.Data[i] / (this.Texture.Width / this.Level.TileWidth)
                        ),
                        new Vector2(x * this.Level.TileWidth, y * this.Level.TileHeight),
                        this.Data[i]
                    );

                    this.TileMap[y, x].Body.Tag = "tile";

                }

            }

        }
    }
}
