using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaDetec
{
    public class RectangleCreator
    {
        public List<SigamDetec.Point> ListOfPoints;
        public RectangleCreator()
        {
            ListOfPoints = new List<SigamDetec.Point>();
        }
        public void AddPoint(SigamDetec.Point pointToAdd)
        {
            ListOfPoints.Add(pointToAdd);
        }
        public Rectangle CreateRectangle()
        {
            if (ListOfPoints.Count == 2)
            {
                var height = ListOfPoints[0].X - ListOfPoints[1].X;
                var width = ListOfPoints[0].Y - ListOfPoints[1].Y;

                var averageDistance = Math.Abs((height + width) / 2);
                
                return new Rectangle((int)ListOfPoints[0].X, (int)ListOfPoints[0].Y, (int)averageDistance, (int)averageDistance);
            }
            else
            {
                return new Rectangle(0, 0, 0, 0);

            }
        }
    }
}
