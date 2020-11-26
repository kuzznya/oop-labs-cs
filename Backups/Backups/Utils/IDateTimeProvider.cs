using System;

namespace Backups.Utils
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTime();
    }
}