using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace AISystem.Pathing
{
    public static class PathGenerator
    {
        public static Path GetPath(Vector3[] corners, Vector3 startingHeading, Vector3 finalHeading)
        {
            if (corners.Length >= 2)
            {
                List<BezierKnot> knots = new List<BezierKnot>(corners.Length);

                knots.Add(new BezierKnot(
                    corners[0],
                    -startingHeading,
                    SplineUtility.GetAutoSmoothTangent(corners[0], corners[1])
                    ));

                for (int i = 1; i < corners.Length - 1; ++i)
                {
                    knots.Add(new BezierKnot(
                        corners[i],
                        SplineUtility.GetAutoSmoothTangent(corners[i + 1], corners[i], corners[i - 1], 1f),
                        SplineUtility.GetAutoSmoothTangent(corners[i - 1], corners[i], corners[i + 1], 1f)
                    ));
                }

                knots.Add(new BezierKnot(
                    corners[^1],
                    -finalHeading,
                    finalHeading
                    ));

                Spline pathSpline = new Spline(knots);
                return new Path(pathSpline, corners[^1], finalHeading);
            }
            return Path.Empty;
        }
    }
}
