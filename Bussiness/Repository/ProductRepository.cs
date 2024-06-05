using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataContext.Data;
using DataContext;
using DataContext.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Business.Repository.IRepository;

public class ProductRepository: IProductRepository
{
    private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductCategory)
            .ToListAsync();

        var productDtos = new List<ProductDTO>();

        foreach (var product in products)
        {
            var productDto = _mapper.Map<ProductDTO>(product);

            // Отримання атрибутів продукту
            var attributeProducts = await _context.AttributeProducts
            .Include(ap => ap.Attribute)
            .Include(ap => ap.AttributeValue)
            .Where(ap => ap.ProductId == product.ProductId)
            .ToListAsync();

            var attributes = new List<DTOs.AttributeDTO>();
            foreach (var attributeProduct in attributeProducts)
            {
                var attributeName = attributeProduct.Attribute?.AttributeName;
                var attributeValue = attributeProduct.AttributeValue?.AttributeValue1;
                if (!string.IsNullOrEmpty(attributeName) && !string.IsNullOrEmpty(attributeValue))
                {
                    var attributeDto = new DTOs.AttributeDTO
                    {
                        AttributeName = attributeName,
                        AttributeValue = attributeValue
                    };
                    attributes.Add(attributeDto);
                }
            }

            productDto.Attributes = attributes;

            // Отримання доступних продуктів в магазинах
            var availableProducts = await _context.AvailableProducts
            .Include(ap => ap.Store)
            .Where(ap => ap.ProductId == product.ProductId)
            .ToListAsync();

            var availableProductsDto = new List<DTOs.AvailableProductDTO>();
            foreach (var availableProduct in availableProducts)
            {
                var availableProductDto = new DTOs.AvailableProductDTO
                {
                    StoreId = availableProduct.StoreId.GetValueOrDefault(),
                    StoreName = availableProduct.Store?.StoreName,
                    Quantity = availableProduct.Quantity.GetValueOrDefault()
                };

                availableProductsDto.Add(availableProductDto);
            }

            productDto.AvailableProducts = availableProductsDto;

            var category = product.ProductCategory;
            var categoryName = category?.CategoryName;
            while (category?.CategoryParent != null && category?.CategoryParentId != 1)
            {
                category = category.CategoryParent;
                categoryName = category.CategoryName + " > " + categoryName;
            }

            productDto.CategoryName = categoryName;

            productDtos.Add(productDto);
        }

        return productDtos;
    }

      

    public async Task<ProductDTO> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            return null; // або можна кинути помилку, якщо продукт не знайдено
        }

        var productDto = _mapper.Map<ProductDTO>(product);

        // Отримання атрибутів продукту
        var attributeProducts = await _context.AttributeProducts
            .Include(ap => ap.Attribute)
            .Include(ap => ap.AttributeValue)
            .Where(ap => ap.ProductId == productId)
            .ToListAsync();

        var attributes = new List<DTOs.AttributeDTO>();
        foreach (var attributeProduct in attributeProducts)
        {
            var attributeName = attributeProduct.Attribute?.AttributeName;
            var attributeValue = attributeProduct.AttributeValue?.AttributeValue1;
            if (!string.IsNullOrEmpty(attributeName) && !string.IsNullOrEmpty(attributeValue))
            {
                var attributeDto = new DTOs.AttributeDTO
                {
                    AttributeName = attributeName,
                    AttributeValue = attributeValue
                };
                attributes.Add(attributeDto);
            }
        }

        productDto.Attributes = attributes;

        // Отримання доступних продуктів в магазинах
        var availableProducts = await _context.AvailableProducts
            .Include(ap => ap.Store)
            .Where(ap => ap.ProductId == productId)
            .ToListAsync();

        var availableProductsDto = new List<DTOs.AvailableProductDTO>();
        foreach (var availableProduct in availableProducts)
        {
            var availableProductDto = new DTOs.AvailableProductDTO
            {
                StoreId = availableProduct.StoreId.GetValueOrDefault(),
                StoreName = availableProduct.Store?.StoreName,
                Quantity = availableProduct.Quantity.GetValueOrDefault()
            };

            availableProductsDto.Add(availableProductDto);
        }

        productDto.AvailableProducts = availableProductsDto;

        var category = product.ProductCategory;
        var categoryName = category?.CategoryName;
        while (category?.CategoryParent != null && category?.CategoryParentId != 1)
        {
            category = category.CategoryParent;
            categoryName = category.CategoryName + " > " + categoryName;
        }

        productDto.CategoryName = categoryName;

        return productDto;
    }

    public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
    {
        // Мапування DTO на модель продукту
        var product = _mapper.Map<Product>(productDto);

        // Отримання бренду за BrandId та встановлення зв'язку
        if (productDto.BrandId != null)
        {
            var brand = await _context.Brands.FindAsync(productDto.BrandId);
            if (brand != null)
            {
                product.Brand = brand;
                productDto.BrandName = brand.BrandName;
            }
        }

        // Отримання категорії за ProductCategoryId та встановлення зв'язку
        if (productDto.ProductCategoryId != null)
        {
            var category = await _context.ProductCategories.FindAsync(productDto.ProductCategoryId);
            if (category != null)
            {
                product.ProductCategory = category;
                productDto.CategoryName = category.CategoryName;
            }
        }

        // Додавання нових атрибутів
        if (productDto.Attributes != null && productDto.Attributes.Any())
        {
            foreach (var attributeDto in productDto.Attributes)
            {
                var attribute = await _context.Attributes.FirstOrDefaultAsync(a => a.AttributeName == attributeDto.AttributeName);
                if (attribute == null)
                {
                    attribute = new DataContext.Models.Attribute { AttributeName = attributeDto.AttributeName };
                    _context.Attributes.Add(attribute);
                }

                var attributeValue = await _context.AttributeValues.FirstOrDefaultAsync(av => av.AttributeValue1 == attributeDto.AttributeValue);
                if (attributeValue == null)
                {
                    attributeValue = new AttributeValue { AttributeValue1 = attributeDto.AttributeValue };
                    _context.AttributeValues.Add(attributeValue);
                }

                var attributeProduct = new AttributeProduct
                {
                    Attribute = attribute,
                    AttributeValue = attributeValue,
                    Product = product
                };

                _context.AttributeProducts.Add(attributeProduct);
            }
        }

        // Додавання інформації про доступність продукту в магазинах
        if (productDto.AvailableProducts != null && productDto.AvailableProducts.Any())
        {
            foreach (var availableProductDto in productDto.AvailableProducts)
            {
                var store = await _context.Stores.FindAsync(availableProductDto.StoreId);
                if (store != null)
                {
                    var availableProduct = new AvailableProduct
                    {
                        Product = product,
                        Store = store,
                        Quantity = availableProductDto.Quantity
                    };


                }

            }
        }

        // Додавання продукту та збереження змін до бази даних
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto)
    {
        var existingProduct = await _context.Products
            .Include(p => p.AttributeProducts)
            .Include(p => p.AvailableProducts)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (existingProduct == null)
            return null;

        // Оновлення властивостей продукту з DTO
        _mapper.Map(productDto, existingProduct);

        // Оновлення зв'язаних сутностей (бренду та категорії)
        if (productDto.BrandId != null)
        {
            var brand = await _context.Brands.FindAsync(productDto.BrandId);
            if (brand != null)
            {
                existingProduct.Brand = brand;
                productDto.BrandName = brand.BrandName;
            }
        }

        if (productDto.ProductCategoryId != null)
        {
            var category = await _context.ProductCategories.FindAsync(productDto.ProductCategoryId);
            if (category != null)
            {
                existingProduct.ProductCategory = category;
                productDto.CategoryName = category.CategoryName;
            }
        }

        // Видалення існуючих атрибутів продукту
        _context.AttributeProducts.RemoveRange(existingProduct.AttributeProducts);

        // Додавання нових атрибутів
        if (productDto.Attributes != null && productDto.Attributes.Any())
        {
            foreach (var attributeDto in productDto.Attributes)
            {
                var attribute = await _context.Attributes.FirstOrDefaultAsync(a => a.AttributeName == attributeDto.AttributeName);
                if (attribute == null)
                {
                    attribute = new DataContext.Models.Attribute { AttributeName = attributeDto.AttributeName };
                    _context.Attributes.Add(attribute);
                }

                var attributeValue = await _context.AttributeValues.FirstOrDefaultAsync(av => av.AttributeValue1 == attributeDto.AttributeValue);
                if (attributeValue == null)
                {
                    attributeValue = new AttributeValue { AttributeValue1 = attributeDto.AttributeValue };
                    _context.AttributeValues.Add(attributeValue);
                }

                var attributeProduct = new AttributeProduct
                {
                    Attribute = attribute,
                    AttributeValue = attributeValue,
                    Product = existingProduct
                };

                _context.AttributeProducts.Add(attributeProduct);
            }
        }

        // Збереження змін до бази даних перед видаленням записів про доступність продукту
        await _context.SaveChangesAsync();

        // Видалення існуючих записів про доступність продукту після збереження змін
        _context.AvailableProducts.RemoveRange(await _context.AvailableProducts.Where(ap => ap.ProductId == productId).ToListAsync());

        // Додавання нових записів про доступність продукту
        if (productDto.AvailableProducts != null && productDto.AvailableProducts.Any())
        {
            foreach (var availableProductDto in productDto.AvailableProducts)
            {
                var store = await _context.Stores.FindAsync(availableProductDto.StoreId);
                if (store != null)
                {
                    var availableProduct = new AvailableProduct
                    {
                        Product = existingProduct,
                        Store = store,
                        Quantity = availableProductDto.Quantity
                    };

                    _context.AvailableProducts.Add(availableProduct);
                }
            }
        }

        // Збереження змін до бази даних після додавання нових записів про доступність продукту
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDTO>(existingProduct);
    }


    public async Task<bool> DeleteProductAsync(int productId)
    {
        // Знаходимо всі записи про доступність, де ProductID має значення NULL
        var orphanedAvailableProducts = await _context.AvailableProducts
            .Where(ap => ap.ProductId == null)
            .ToListAsync();

        // Видаляємо всі знайдені записи про доступність
        _context.AvailableProducts.RemoveRange(orphanedAvailableProducts);

        // Знаходимо продукт для видалення разом із його дочірніми записами
        var existingProduct = await _context.Products
            .Include(p => p.AttributeProducts)
            .Include(p => p.AvailableProducts)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (existingProduct == null)
            return false;

        // Видаляємо всі зв'язані записи про доступність продукту
        _context.AvailableProducts.RemoveRange(existingProduct.AvailableProducts);

        // Видаляємо всі зв'язані записи про атрибути продукту
        _context.AttributeProducts.RemoveRange(existingProduct.AttributeProducts);

        // Видаляємо сам продукт
        _context.Products.Remove(existingProduct);

        await _context.SaveChangesAsync();

        return true;
    }


}
