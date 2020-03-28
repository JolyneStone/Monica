﻿using KiraNet.AlasFx.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace KiraNet.AlasFx.Domain
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 保存更新操作，适用于大批量插入、更新、删除操作
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="options"></param>
        public static void BulkSaveChanges(this IUnitOfWork unitOfWork, Action<BulkOperation> options = null)
        {
            Check.NotNull(unitOfWork, nameof(unitOfWork));
            var dbContext = unitOfWork.DbContext.AsDbContext();
            if (options == null)
                dbContext.BulkSaveChanges();
            else
                dbContext.BulkSaveChangesAsync(options);
        }

        /// <summary>
        /// 异步保存更新操作，适用于大批量插入、更新、删除操作
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static async Task BulkSaveChangesAsync(this IUnitOfWork unitOfWork, Action<BulkOperation> options = null)
        {
            Check.NotNull(unitOfWork, nameof(unitOfWork));
            var dbContext = unitOfWork.DbContext.AsDbContext();
            if (options == null)
                await dbContext.BulkSaveChangesAsync();
            else
                await dbContext.BulkSaveChangesAsync(options);
        }
    }
}
