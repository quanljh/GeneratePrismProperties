//-------------------------------------------------------------------
// Copyright (c) 2020 EMSystems LTD. All Rights Reserved.
// ネームスペース      ：GeneratePrismProperties.Extensions
// ファイル名称        ：VisualTreeExtensions
// 新規作成者          ：EM-劉嘉豪
// 新規作成日          ：2020/09/22 11:18:02
// ファイルメモ        ：

/*-< 変更履歴 >------------------------------------------------------
*/
//-------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GeneratePrismProperties
{
    public static class VisualTreeExtensions
    {
        internal static DependencyObject FindVisualTreeRoot(this DependencyObject d)
        {
            var current = d;

            var result = d;

            while (current != null)
            {
                result = current;
                if (current is Visual || current is Visual3D)
                    break;
                // If we're in Logical Land then we must walk up the logical tree
                // until we find a Visual/Visual3D to get us back to Visual Land.
                current = LogicalTreeHelper.GetParent(current);
            }

            return result;
        }

        public static T FindVisualParent<T>(this DependencyObject db) where T : class

        {
            var parent = VisualTreeHelper.GetParent(db.FindVisualTreeRoot());
            while (parent != null)
            {
                if (parent is T element)
                    return element;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T item)
                    return item;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null) return childOfChild;
                }
            }
            return null;
        }

        public static T FindVisualChild<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                var item = child as T;
                if (item?.Name == name)
                {
                    return item;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null) return childOfChild;
                }
            }
            return null;
        }

    }
}