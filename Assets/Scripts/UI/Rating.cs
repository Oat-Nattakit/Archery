using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour {

	public int rateLevel(string lvl, int bullet){		
		int nbrStar = 0;
		LevelData data = DataManager.instance.GameConfig.GetDataLevel(lvl);
		RatingConfig ratingData = data.Rating;

        if (bullet >= ratingData.MaxStar)
        {
			nbrStar = 3;
        }
		else if (bullet >= ratingData.HalfStar)
        {
			nbrStar = 2;
		}
		else if (bullet >= ratingData.MinStar)
        {
			nbrStar = 1;
		}		
		return nbrStar;
	}	
}
