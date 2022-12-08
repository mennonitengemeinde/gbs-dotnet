﻿namespace Gbs.Application.Common.Interfaces;

public interface ICrudCommand<T, in TCreate, in TUpdate>
{
    Task<Result<T>> Add(TCreate request);
    Task<Result<T>> Update(TUpdate request);
    Task<Result<bool>> Delete(int id);
}