using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Shapes;
using VauxGame.Components;
using VauxGame.Components.Implementations;
using VauxGame.Core.Infrastructure.Helpers;
using VauxGame.Utils;

namespace VauxGame
{
    public class WorldComponent : IComponent
    {
        #region - Fields -

        private TiledMap _map;
        private readonly Camera2D _camera;

        #endregion

        #region - Properties -

        public ComponentTypeEnum ComponentType => ComponentTypeEnum.World;

        public bool IsVisible => true;

        public int WidthInPixels => _map.WidthInPixels;
        public int HeightInPixels => _map.HeightInPixels;
        public RectangleF[] Collisions { get; set; }

        #endregion

        #region - Constructors -

        public WorldComponent(Camera2D camera)
        {
            _camera = camera;
        }

        #endregion

        #region - Public methods -

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _map = content.Load<TiledMap>("maps/map2");

            var trees = _map.GetObjectGroup("Trees").Objects;
            Collisions = trees.GetRectangles().ToArray();
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var layer in _map.Layers.Where(l => l.Name != "UpperLayer"))
            {
                layer.IsVisible = true;
            }
            _map.GetLayer("UpperLayer").IsVisible = false;

            _map.Draw(_camera.GetViewMatrix());
        }

        public void DrawUpperLayer(SpriteBatch spriteBatch)
        {
            foreach (var layer in _map.Layers.Where(l => l.Name != "UpperLayer"))
            {
                layer.IsVisible = false;
            }

            var upperLayer = _map.GetLayer("UpperLayer");
            upperLayer.IsVisible = true;

            InGameDebugger.AppendLine($"Opacity: {upperLayer.Opacity}");
            InGameDebugger.AppendLine($"Visibility: {upperLayer.IsVisible}");

            //_map.Draw(_camera.GetViewMatrix());

            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                transformMatrix: _camera.GetViewMatrix()
            );

            foreach (var layer in _map.TileLayers.Where(t => t.Name.Equals("UpperLayer")))
            {
                layer.Draw(spriteBatch);
            }

            // Collisions DEBUG
            foreach (var rectangleF in Collisions)
            {
                spriteBatch.DrawRectangle(rectangleF, Color.Aqua);
            }
            spriteBatch.End();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public void UpdateUpperLayerVisibility(Player player)
        {
            var upperLayer = _map.TileLayers
                .First(l => l.Name.Equals("UpperLayer"));
            var upperLayerTiles = from tile in upperLayer.Tiles
                where tile.Id != 0
                select new RectangleF(
                    x: tile.X*upperLayer.TileWidth,
                    y: tile.Y*upperLayer.TileHeight,
                    width: upperLayer.TileWidth,
                    height: upperLayer.TileHeight);
            var isCollidingWithPlayer = upperLayerTiles.Any(t => t.Intersects(player.BoundingBox));

            InGameDebugger.AppendLine($"IsColliding: {isCollidingWithPlayer}");

            upperLayer.Opacity = isCollidingWithPlayer ? 0.25f : 1f;
            //upperLayer.IsVisible = !isCollidingWithPlayer;
        }

        #endregion
    }
}

