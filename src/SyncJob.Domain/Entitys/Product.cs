﻿
using Consts;
using ETO;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Entitys
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// A unique value for this product.
        /// ProductManager ensures the uniqueness of it.
        /// It can not be changed after creation of the product.
        /// </summary>
        [NotNull]
        [StringLength(128)]
        public string Code { get; private set; }

        [NotNull]
        [StringLength(128)]
        public string Name { get; private set; }

        public float Price { get; private set; }

        public int StockCount { get; private set; }

        [StringLength(128)]
        public string ImageName { get; private set; }

        private Product()
        {
            //Default constructor is needed for ORMs.
        }

        internal Product(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            float price = 0.0f,
            int stockCount = 0,
            string imageName = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            if (code.Length >= ProductConsts.MaxCodeLength)
            {
                throw new ArgumentException($"Product code can not be longer than {ProductConsts.MaxCodeLength}");
            }

            Id = id;
            Code = code;
            SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
            SetPrice(price);
            SetImageName(imageName);
            SetStockCountInternal(stockCount, triggerEvent: false);
        }

        public Product SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= ProductConsts.MaxNameLength)
            {
                throw new ArgumentException($"Product name can not be longer than {ProductConsts.MaxNameLength}");
            }

            Name = name;
            return this;
        }

        public Product SetImageName([CanBeNull] string imageName)
        {
            if (imageName == null)
            {
                return this;
            }

            if (imageName.Length >= ProductConsts.MaxImageNameLength)
            {
                throw new ArgumentException($"Product image name can not be longer than {ProductConsts.MaxImageNameLength}");
            }

            ImageName = imageName;
            return this;
        }

        public Product SetPrice(float price)
        {
            if (price < 0.0f)
            {
                throw new ArgumentException($"{nameof(price)} can not be less than 0.0!");
            }

            Price = price;
            return this;
        }

        public Product SetStockCount(int stockCount)
        {
            return SetStockCountInternal(stockCount);
        }

        private Product SetStockCountInternal(int stockCount, bool triggerEvent = true)
        {
            if (StockCount < 0)
            {
                throw new ArgumentException($"{nameof(stockCount)} can not be less than 0!");
            }

            if (StockCount == stockCount)
            {
                return this;
            }

            if (triggerEvent)
            {
                AddDistributedEvent(
                    new ProductStockCountChangedEto(
                        Id,
                        StockCount,
                        stockCount
                    )
                );
            }

            StockCount = stockCount;
            return this;
        }
    }
}
