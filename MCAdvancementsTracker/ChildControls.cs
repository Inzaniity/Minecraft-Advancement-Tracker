﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MCAdvancementsTracker
{
    class ChildControls
    {
        private List<object> lstChildren;

        public List<object> GetChildren(Visual p_vParent, int p_nLevel)
        {
            if (p_vParent == null)
            {
                throw new ArgumentNullException("Element {0} is null!", p_vParent.ToString());
            }

            this.lstChildren = new List<object>();

            this.GetChildControls(p_vParent, p_nLevel);

            return this.lstChildren;

        }

        private void GetChildControls(Visual p_vParent, int p_nLevel)
        {
            int nChildCount = 0;
            p_vParent.Dispatcher.Invoke(new Action(() =>
            {
                nChildCount = VisualTreeHelper.GetChildrenCount(p_vParent);
            }));


            for (int i = 0; i <= nChildCount - 1; i++)
            {
                Visual v = null;
                p_vParent.Dispatcher.Invoke(new Action(() =>
                {
                    v = (Visual)VisualTreeHelper.GetChild(p_vParent, i);
                    lstChildren.Add((object)v);

                    if (VisualTreeHelper.GetChildrenCount(v) > 0)
                    {
                        GetChildControls(v, p_nLevel + 1);
                    }
                }));
            }
        }
    }
}
