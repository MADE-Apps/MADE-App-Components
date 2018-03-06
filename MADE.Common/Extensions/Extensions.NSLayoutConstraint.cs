﻿#if __IOS__
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.NSLayoutConstraint.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for NSLayoutConstraint values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE
{
    using Foundation;

    using UIKit;

    /// <summary>
    /// Defines a collection of extensions for <see cref="NSLayoutConstraint"/> values.
    /// </summary>
    public static partial class Extensions
    {
        private static readonly NSString ConstantKey = new NSString("ConstantKey");
        private static readonly NSString DidCreateKey = new NSString("DidCreateKey");

        /// <summary>
        /// Clears the constraint's constant value.
        /// </summary>
        /// <param name="constraint">
        /// The constraint to clear.
        /// </param>
        /// <param name="didCreate">
        /// A value indicating whether did create.
        /// </param>
        public static void Clear(this NSLayoutConstraint constraint, bool didCreate)
        {
            if (constraint.Constant != 0)
            {
                NSNumber oldConstant = new NSNumber(constraint.Constant);
                constraint.Constant = 0;

                constraint.SetAssociatedObject(ConstantKey, oldConstant);
            }

            constraint.SetAssociatedObject(DidCreateKey, (NSNumber)didCreate);
        }


        /// <summary>
        /// Restores the constraint's constant value, if it is stored as an associated object. 
        /// </summary>
        /// <param name="constraint">
        /// The constraint to restore.
        /// </param>
        /// <returns>
        /// Returns true if the constraint was created for the purpose of collapsing the view.
        /// </returns>
        public static bool Restore(this NSLayoutConstraint constraint)
        {
            NSNumber oldConstant = (NSNumber)constraint.GetAssociatedObject(ConstantKey);
            NSNumber didCreate = (NSNumber)constraint.GetAssociatedObject(DidCreateKey);

            if (oldConstant != null)
            {
                constraint.Constant = oldConstant.FloatValue;
                constraint.SetAssociatedObject(ConstantKey, null);
            }

            if (didCreate == null)
            {
                return false;
            }

            constraint.SetAssociatedObject(DidCreateKey, null);
            return didCreate.BoolValue;
        }
    }
}
#endif