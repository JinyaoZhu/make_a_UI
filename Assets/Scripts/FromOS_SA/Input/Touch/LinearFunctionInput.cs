using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Evaluate input accordning to linuar functions (y=mx+b).
/// y = value 
/// x = argument
/// m = gradient
/// b = directComponent
/// </summary>
public class LinearFunctionInput {
    
    /// <summary>
    /// Validate a lin. function. Use this function if deltaX != deltaY.
    /// </summary>
    /// <param name="storedInteraction">Interaction transformd to 2D vectors.</param>
    /// <param name="deltaX">Delta between first and last x value.</param>
    /// <param name="deltaY">Delta between first and last y value. </param>
    /// <param name="epsilon">Devitation.</param>
    /// <returns>The direction of the interaction.</returns>
    public string ValidateInteraktion_dxdy_diff (List<Vector2> storedInteraction, float deltaX, float deltaY, float epsilon) {
        string validationResult = "Unknown";
        Vector2 delta = storedInteraction[storedInteraction.Count-1] - storedInteraction[0]; // lin. difference SP & EP
        float gradient = delta.y/delta.x; // "m"
        float directComponent = storedInteraction[0].y - gradient * storedInteraction[0].x; // "b"
        if (Math.Abs(delta.x) >= deltaX && Math.Abs(delta.y) <= deltaY) {
            return Validate(storedInteraction, deltaX, deltaY, epsilon, delta, gradient, directComponent);
        }
        else return validationResult;
    }

    /// <summary>
    /// Validate a lin. function. Use this function if deltaX == deltaY.
    /// </summary>
    /// <param name="storedInteraction">Interaction transformd to 2D vectors.</param>
    /// <param name="deltaX">Delta between first and last x value.</param>
    /// <param name="deltaY">Delta between first and last y value. </param>
    /// <param name="epsilon">Devitation.</param>
    /// <returns>The direction of the interaction.</returns>
	public string ValidateInteraktion_dxdy_same (List<Vector2> storedInteraction, float deltaX, float deltaY, float epsilon) {
		string validationResult = "Unknown";
		Vector2 delta = storedInteraction[storedInteraction.Count-1] - storedInteraction[0]; // lin. difference SP & EP
		float gradient = delta.y/delta.x; // "m"
		float directComponent = storedInteraction[0].y - gradient * storedInteraction[0].x; // "b"
		if (Math.Abs(delta.x) >= deltaX && Math.Abs(delta.y) >= deltaY) {
            return Validate(storedInteraction, deltaX, deltaY, epsilon, delta, gradient, directComponent);
		} // TODO: Einfach weitere elseif fall? sollte doch klappen. Dann nicht so viel codedopplung.
		else return validationResult;
	}

    private string Validate (List<Vector2> storedInteraction, float deltaX, float deltaY, float epsilon, Vector2 delta, float gradient, float directComponent) {
        string validationResult = "Unknown";
        // validating: rough
        if (delta.x <= 0) {
            validationResult = "negativ";
        }
        else validationResult = "positiv";
        // validating: fine
        foreach (Vector2 element in storedInteraction) {
            if (!CheckLinearFunction(gradient, directComponent, element.x, element.y, epsilon)) {
                // Validation fails after one point isn't in epsilon envirement.
                validationResult = "Unknown";
                break;
            }
        }
        return validationResult;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="storedInteraction">Interaction transformd to 2D vectors.</param>
    /// <param name="deltaX">Delta between first and last x value.</param>
    /// <param name="deltaY">Delta between first and last y value. </param>
    /// <param name="epsilon">Devitation.</param>
    /// <returns>The direction of the interaction.</returns>
	public string ValidateInteraktion2Parts (List<Vector2> storedInteraction, float deltaX, float deltaY, float epsilon) {
		string validationResult = "Unknown";
		Vector2 delta = storedInteraction[storedInteraction.Count-1] - storedInteraction[0]; // lin. difference SP & EP
		Vector2 lowestY = storedInteraction [0];
		//int lowestYListIndex = 0; 
		int counter = 0;
		// get Point with lowest y value
		foreach (Vector2 element in storedInteraction) {
			if (lowestY.y > element.y) {
				lowestY = element;
			}
			counter++;
		}

		// two vertical liniear functions: y=mx+b
		float gradient1 = (storedInteraction[0].y - lowestY.y) / (storedInteraction[0].x - lowestY.x); // "m1"
		float directComponent1 = storedInteraction[0].y - gradient1 * storedInteraction[0].x; // "b1"
		float gradient2 = (lowestY.y - storedInteraction[storedInteraction.Count-1].y) / (lowestY.x - storedInteraction[storedInteraction.Count-1].x); // Calculating gradient2, because of tracking stability. Theoreticly m1=-m2
		float directComponent2 = storedInteraction[storedInteraction.Count-1].y - gradient2 * storedInteraction[storedInteraction.Count-1].x; // "b2"

		// validating: rough
		if (delta.x <= 0) {
			//TODO: In Private Funktion auslagern. Codedopplung!
			validationResult = "negativ";
		} else validationResult = "positiv";
		// validating: fine
		foreach (Vector2 element in storedInteraction) {
			if (!CheckLinearFunction(gradient1, directComponent1, element.x, element.y, epsilon*2)) {
				if(!CheckLinearFunction(gradient2, directComponent2, element.x, element.y, epsilon)) {
					validationResult = "Unknown" ;
					break;
				}
			}
		}
		return validationResult;
	}

    /// <summary>  
    /// Check when point is in an epsilon enviroment of the liniar function.
    /// </summary> 
    /// <param name="gradient">Graditent "m" in liniar function</param>
    /// <param name="directComponetn">Direct component "b" in liniar function</param>
    /// <param name="xValue">Point: x value</param>
    /// <param name="yValue">Point: y value</param>
    /// <param name="epsilon">Epsilon envirement</param>
    /// <returns></returns>
    private Boolean CheckLinearFunction(float gradient, float directComponent, float xValue, float yValue, float epsilon) {
        float localeY = gradient * xValue + directComponent; // calculate locale y value in reference to given x value
        return (Math.Abs(localeY - yValue) <= epsilon);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="storedInteraction"></param>
    /// <returns></returns>
	public int DetectXpart (List<Vector2> storedInteraction) {

		float deltaX = 200;
		float deltaY = 200;
		float epsilon = 50;

		int returnResult = 2;

		Vector2 delta = storedInteraction[storedInteraction.Count-1] - storedInteraction[0]; // lin. difference SP & EP
		float gradient = delta.y/delta.x; // "m"
		float directComponent = storedInteraction[0].y - gradient * storedInteraction[0].x; // "b"

		if (Math.Abs(delta.x) >= deltaX && Math.Abs(delta.y) >= deltaY) {
			// Diagonal : /
			if (Math.Sign (delta.x) == Math.Sign (delta.y)) {
				returnResult = 2;
			} else { // Diagonal: \
				returnResult = 4;
			}
				
			foreach (Vector2 element in storedInteraction) {
				if (!CheckLinearFunction(gradient, directComponent, element.x, element.y, epsilon)) {
					returnResult = 8;
					break;
				}
			}
			return returnResult;
		}
		else return 8;
  	}
}
