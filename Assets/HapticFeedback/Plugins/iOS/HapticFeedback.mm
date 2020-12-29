#include "HapticFeedback.h"


UISelectionFeedbackGenerator *selectionFeedback = nil;

UIImpactFeedbackGenerator *impactFeedbackLight = nil;
UIImpactFeedbackGenerator *impactFeedbackMedium = nil;
UIImpactFeedbackGenerator *impactFeedbackHeavy = nil;

UINotificationFeedbackGenerator *notificationFeedback = nil;

extern "C" {
	
    // selection
    void _initSelectionFeedback () {
        if(selectionFeedback == nil)
            selectionFeedback = [[UISelectionFeedbackGenerator alloc] init];
    }
	
	void _prepareSelectionFeedback () {
        [selectionFeedback prepare];
	}
    
    void _selectionFeedback () {
        [selectionFeedback selectionChanged];
    }
    
    // impact
    void _initImpactFeedback (int strength) {
        
        if(strength == 0)
        {
            if(impactFeedbackLight == nil)
                impactFeedbackLight = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
        }
        else if(strength == 1)
        {
            if(impactFeedbackMedium == nil)
                impactFeedbackMedium = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
        }
        else
        {
            if(impactFeedbackHeavy == nil)
                impactFeedbackHeavy = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleHeavy];
        }
    }
    
    void _prepareImpactFeedback (int strength) {
        if(strength == 0)
            [impactFeedbackLight prepare];
        else if(strength == 1)
            [impactFeedbackMedium prepare];
        else
            [impactFeedbackHeavy prepare];
    }
    
    void _impactFeedback (int strength) {
        if(strength == 0)
            [impactFeedbackLight impactOccurred];
        else if(strength == 1)
            [impactFeedbackMedium impactOccurred];
        else
            [impactFeedbackHeavy impactOccurred];
    }
    
    // notification
    
    void _initNotificationFeedback () {
        if(notificationFeedback == nil)
            notificationFeedback = [[UINotificationFeedbackGenerator alloc] init];
    }
    
    void _prepareNotificationFeedback () {
        [notificationFeedback prepare];
    }
    
    void _notificationFeedbackSuccess () {
        [notificationFeedback notificationOccurred:UINotificationFeedbackTypeSuccess];
    }
    
    void _notificationFeedbackError () {
        [notificationFeedback notificationOccurred:UINotificationFeedbackTypeError];
    }
    
    void _notificationFeedbackWarning () {
        [notificationFeedback notificationOccurred:UINotificationFeedbackTypeWarning];
    }
}










