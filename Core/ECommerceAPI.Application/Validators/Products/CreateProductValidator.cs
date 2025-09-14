using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Lütfen ürün adını doldurunuz.")
                .NotNull()
                    .WithMessage("Lütfen ürün adını doldurunuz.")
                .MinimumLength(5)
                    .WithMessage("Lütfen ürün adını 5 ile 150 karakter aralığında doldurunuz.")
                .MaximumLength(150)
                    .WithMessage("Lütfen ürün adını 5 ile 150 karakter aralığında doldurunuz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                    .WithMessage("Lütfen stok bilgisini doldurunuz.")
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini doldurunuz.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi sıfırdan (0) küçük olamaz");

            RuleFor(p => p.Price)
               .NotEmpty()
                   .WithMessage("Lütfen fiyat bilgisini doldurunuz.")
               .NotNull()
                   .WithMessage("Lütfen fiyat bilgisini doldurunuz.")
               .Must(p => p >= 0)
                   .WithMessage("Fiyat bilgisi sıfırdan (0) küçük olamaz");
        }
    }
}
