using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public sealed record Page<T>(
        IReadOnlyCollection<T> Collection,
        int PageNumber,
        int PageSize,
        int TotalCount);
