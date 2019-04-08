using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookAddIn.Constants
{
    public class CategoryConstant
    {
        #region Class

        public class CategoryItemConstant
        {
            public CategoryItemConstant(int p_SecondLevelCategory, string p_SecondLevelCategoryDescription)
            {
                SecondLevelCategory = p_SecondLevelCategory;
                SecondLevelCategoryDescription = p_SecondLevelCategoryDescription;
            }

            public int SecondLevelCategory { get; private set; }
            public string SecondLevelCategoryDescription { get; private set; }
        }

        public class CategoryGroupItemConstant
        {
            public CategoryGroupItemConstant(int p_PrimaryLevelCategory, string p_PrimaryLevelCategoryDescription, List<CategoryItemConstant>
                p_ListCategorySecondLevel)
            {
                PrimaryLevelCategory = p_PrimaryLevelCategory;
                PrimaryLevelCategoryDescription = p_PrimaryLevelCategoryDescription;
                ListCategorySecondLevel = p_ListCategorySecondLevel;
            }
            public int PrimaryLevelCategory { get; private set; }
            public string PrimaryLevelCategoryDescription { get; private set; }
            public List<CategoryItemConstant> ListCategorySecondLevel { get; private set; }
        }

        #endregion

        public static CategoryGroupItemConstant CoarseGender = new CategoryGroupItemConstant(1, "Coarse Gender",
            new List<CategoryItemConstant>() {
            new CategoryItemConstant(1, "Company Business, Strategy"),
            new CategoryItemConstant(2, "Purely Personal"),
            new CategoryItemConstant(3, "Personal but in professional context (e.g., it was good working with you)"),
            new CategoryItemConstant(4, "Logistic Arrangements (meeting scheduling, technical support, etc)"),
            new CategoryItemConstant(5, "Employment arrangements (job seeking, hiring, recommendations, etc)"),
            new CategoryItemConstant(6, "Document editing/checking (collaboration)"),
            new CategoryItemConstant(7, "Empty message (due to missing attachment)"),
            new CategoryItemConstant(8, "Empty message"),
            });

        public static CategoryGroupItemConstant EmotionalTone = new CategoryGroupItemConstant(4, "Emotional tone",
           new List<CategoryItemConstant>() {
            new CategoryItemConstant(1, "jubilation"),
            new CategoryItemConstant(2, "hope / anticipation"),
            new CategoryItemConstant(3, "humor"),
            new CategoryItemConstant(4, "camaraderie"),
            new CategoryItemConstant(5, "admiration"),
            new CategoryItemConstant(6, "gratitude"),
            new CategoryItemConstant(7, "friendship / affection"),
            new CategoryItemConstant(8, "sympathy / support"),
            new CategoryItemConstant(9, "sarcasm"),
            new CategoryItemConstant(10, "secrecy / confidentiality"),
            new CategoryItemConstant(11, "worry / anxiety"),
            new CategoryItemConstant(12, "concern"),
            new CategoryItemConstant(13, "competitiveness / aggressiveness"),
            new CategoryItemConstant(14, "triumph / gloating"),
            new CategoryItemConstant(15, "pride"),
            new CategoryItemConstant(16, "anger / agitation"),
            new CategoryItemConstant(17, "sadness / despair"),
            new CategoryItemConstant(18, "shame"),
            new CategoryItemConstant(19, "dislike / scorn")
           });

        public static List<CategoryGroupItemConstant> ListCategoryGroupItemData
        {
            get
            {
                List<CategoryGroupItemConstant> v_ListCategoryGroupItemConstant = new List<CategoryGroupItemConstant>();
                v_ListCategoryGroupItemConstant.Add(CoarseGender);
                v_ListCategoryGroupItemConstant.Add(EmotionalTone);
                return v_ListCategoryGroupItemConstant;
            }
        }
}
}
