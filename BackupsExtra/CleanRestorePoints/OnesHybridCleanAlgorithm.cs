using System;
using System.Collections.Generic;

namespace BackupsExtra.CleanRestorePoints
{
    public class OnesHybridCleanAlgorithm : AbstractCleanAlgorithm
    {
        private int _amountRestorePoint;
        private DateTime _finishDate;

        public OnesHybridCleanAlgorithm(int amountRestorePoint, DateTime finishDate)
        {
            _amountRestorePoint = amountRestorePoint;
            _finishDate = finishDate;
        }

        public override List<RestorePointWithDateCreation> GetPointsAfterClean(List<RestorePointWithDateCreation> points)
        {
            var resultList = new List<RestorePointWithDateCreation>();
            for (int i = _amountRestorePoint; i < points.Count; i++)
            {
                if (points[i].Point.CreateDataTime.Subtract(_finishDate).Days > 0)
                {
                    resultList.Add(points[i]);
                }
            }

            return resultList;
        }
    }
}