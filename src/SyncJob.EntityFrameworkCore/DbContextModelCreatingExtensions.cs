using System;
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SyncJob.EntityFrameworkCore
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

            builder.Entity<Book>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Books", options.Schema);
                b.ConfigureExtraProperties();
                //Properties
                //b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

                //Configure relations
                //b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Configure indexes
                b.HasIndex(q => q.LastModificationTime);
            });
            builder.Entity<UserTarget>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Users", options.Schema);
                //Properties
                b.HasKey(q => q.UserID);
                b.Property(q => q.UserID).IsRequired();
                b.Property(q => q.UserCode).IsRequired().HasMaxLength(Consts.DefaultMaxLength);
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