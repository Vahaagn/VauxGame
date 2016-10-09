using System.Collections.Generic;
using System.Linq;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Shapes;

namespace VauxGame.Core.Infrastructure.Helpers
{
    public static class TiledExtensions
    {
        public static IEnumerable<RectangleF> GetRectangles(this TiledObject[] tiledObjects)
            => tiledObjects.Select(GetRectangleF);

        public static RectangleF GetRectangleF(this TiledObject tiledObject)
            => new RectangleF(
                x: tiledObject.X,
                y: tiledObject.Y,
                width: tiledObject.Width,
                height: tiledObject.Height
            );
    }
}