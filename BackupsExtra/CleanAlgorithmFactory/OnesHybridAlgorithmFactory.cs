using System;
using BackupsExtra.CleanRestorePoints;

namespace BackupsExtra.CleanAlgorithmFactory
{
    public class OnesHybridAlgorithmFactory : ICleanAlgorithmFactory
    {
        private int _amountRestorePoint;
        private DateTime _finishDate;
        public OnesHybridAlgorithmFactory(int amountRestorePoint, DateTime finishDate)
        {
            _amountRestorePoint = amountRestorePoint;
            _finishDate = finishDate;
        }

        public TypeCleanPoints GetTypeClean()
        {
            return TypeCleanPoints.OnceHybrid;
        }

        public AbstractCleanAlgorithm Create()
        {
            return new OnesHybridCleanAlgorithm(_amountRestorePoint, _finishDate);
        }
    }
}