using System;
using WebDownloader.Enums;

namespace WebDownloader.Interfaces
{
    public interface IConstraint
    {
        ConstraintType ConstraintType { get; }

        bool IsAcceptable(Uri uri);
    }
}
