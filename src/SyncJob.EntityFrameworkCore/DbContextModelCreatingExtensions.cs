using System;
using Domain;
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EntityFrameworkCore
{
    public static class DbContextModelCreatingExtensions
    {
        public static void ConfigureSyncJob(
            this ModelBuilder builder,
            Action<ModelBuilderConfigOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ModelBuilderConfigOptions();

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                //b.ToTable(options.TablePrefix + "Questions", options.Schema);
                
                //Properties
                //b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Configure relations
                //b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Configure indexes
                //b.HasIndex(q => q.CreationTime);
            });
            */
        }

        public static void ConfigureTargetDb(
            this ModelBuilder builder,
            Action<ModelBuilderConfigOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ModelBuilderConfigOptions();

            optionsAction?.Invoke(options);

            //builder.Entity<Book>(b =>
            //{
            //    //Configure table & schema name
            //    b.ToTable(options.TablePrefix + "Books", options.Schema);
            //    b.ConfigureExtraProperties();
            //    //Properties
            //    //b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //    //Configure relations
            //    //b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //    //Configure indexes
            //    b.HasIndex(q => q.LastModificationTime);
            //});
            builder.Entity<UserTarget>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Users", options.Schema);
                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Id).ValueGeneratedNever();//设置主键不自动递增
                b.Property(q => q.Id).IsRequired().HasColumnName("UserID");
                b.Property(q => q.UserCode).IsRequired().HasMaxLength(Consts.DefaultMaxLength);
                b.Property(q => q.CreationTime).HasColumnName("CreatDate").IsRequired()
                .HasColumnType("datetime").HasDefaultValueSql("(GETDATE())")
               ;
                b.Property(q => q.LastModificationTime).HasColumnName("ModifyDate").IsRequired()
                 .HasColumnType("datetime").HasDefaultValueSql("(GETDATE())")
                ;
                //Configure indexes
                b.HasIndex(q => q.CompanyID);
            });

        }

        public static void ConfigureTargetDbMysql(
            this ModelBuilder builder,
            Action<ModelBuilderConfigOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ModelBuilderConfigOptions();

            optionsAction?.Invoke(options);

            //builder.Entity<Book>(b =>
            //{
            //    //Configure table & schema name
            //    b.ToTable(options.TablePrefix + "Books", options.Schema);
            //    b.ConfigureExtraProperties();
            //    //Properties
            //    //b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //    //Configure relations
            //    //b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //    //Configure indexes
            //    b.HasIndex(q => q.LastModificationTime);
            //});
            builder.Entity<UserTarget>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Users", options.Schema);
                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Id).ValueGeneratedNever();//设置主键不自动递增
                b.Property(q => q.Id).IsRequired().HasColumnName("UserID");
                b.Property(q => q.UserCode).IsRequired().HasMaxLength(Consts.DefaultMaxLength);
                b.Property(q => q.CreationTime).HasColumnName("CreatDate").IsRequired()
                .HasColumnType("datetime").HasDefaultValueSql("Now()");
                b.Property(q => q.LastModificationTime).HasColumnName("ModifyDate").IsRequired()
                 .HasColumnType("datetime").HasDefaultValueSql("Now()");
                //Configure indexes
                b.HasIndex(q => q.CompanyID);
            });

        }



        public static void ConfigureSourceDb(
            this ModelBuilder builder,
            Action<ModelBuilderConfigOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ModelBuilderConfigOptions();

            optionsAction?.Invoke(options);

        }
    }
}