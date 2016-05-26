using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Gonzigonz.SampleApp.Data.Context;

namespace WebApp.Migrations
{
	[DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gonzigonz.SampleApp.Domain.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTimeUTC");

                    b.Property<string>("Details");

                    b.Property<bool>("IsComplete");

                    b.Property<DateTime>("ModifiedTimeUTC");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");
                });
        }
    }
}
