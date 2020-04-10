using System;
using System.Collections.Generic;
using System.Linq;
using WebDownloader.Enums;
using WebDownloader.Interfaces;

namespace WebDownloader.Constraints
{
    public class FileExtensionConstraint : IConstraint
    {
        private readonly IEnumerable<string> _acceptableExtensions;

        public FileExtensionConstraint(IEnumerable<string> acceptableExtensions)
        {
            _acceptableExtensions = acceptableExtensions;
        }

        public ConstraintType ConstraintType => ConstraintType.FileConstraint;

        public bool IsAcceptable(Uri uri)
        {
            string lastSegment = uri.Segments.Last();
            return _acceptableExtensions.Any(e => lastSegment.EndsWith(e));
        }
    }
}
