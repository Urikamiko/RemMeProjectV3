using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.WindowActions
{
    public class Pagination<T>
    {
        public const int ITEM_COUNT = 10;
        public int maxPage;
        public int currentPage=1;
        public Pagination() {
        }
        public List<T> GetItems(List<T> items) {
            List<T> allItems = new List<T>();
            foreach (T i in items)
            {
                allItems.Add(i);
            }
            maxPage = allItems.Count / ITEM_COUNT + 1;
            List<T> currentItems = new List<T>();
            if (currentPage * ITEM_COUNT >= allItems.Count) {
                for (int i = (currentPage - 1) * ITEM_COUNT; i < allItems.Count; i++) { 
                currentItems.Add(allItems[i]);
                }
            }
            else {
                for (int i = (currentPage - 1) * ITEM_COUNT; i < currentPage * ITEM_COUNT; i++) {
                    currentItems.Add(allItems[i]);
                }
            }
            return currentItems; 
        }
        public void NextPage() {
            if (currentPage < maxPage)
            {
                currentPage++;
            }
            else {
                currentPage = maxPage;
            }
        }
        public void PrevPage() {
            currentPage--;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
        }
        public bool isNext() {
            return currentPage < maxPage;
        }
        public bool isPrev()
        {
            return currentPage > 1;
        }
    }
}
