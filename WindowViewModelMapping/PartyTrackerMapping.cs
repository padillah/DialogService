using PartyTracker.AttendancePopup;
using PartyTracker.Main;
using PartyTracker.MemberPopup;
using PartyTracker.NotePopup;
using PartyTracker.Popups.MergeMemberPopup;
using PartyTracker.ReservationHistoryPopup;
using PartyTracker.ReservationPopup;
using ServiceLocator.WindowViewModelMapping;
using System;
using System.Collections.Generic;

namespace PartyTracker.WindowViewModelMapping
{
    public class PartyTrackerMapping : WindowViewModelMappings
    {
        public PartyTrackerMapping()
        {
            Mappings = new Dictionary<Type, Type>
                       {
                           { typeof(MainViewModel), typeof(string) } 
                           ,{ typeof(NewReservationViewModel), typeof(ReservationView) }
                           ,{ typeof(EditReservationViewModel), typeof(ReservationView) }
                           ,{ typeof(NewMemberViewModel), typeof(MemberView) } 
                           ,{ typeof(EditMemberViewModel), typeof(MemberView) }
                           ,{ typeof(EditAttendanceViewModel), typeof(AttendanceView) }
                           ,{ typeof(EditNoteViewModel), typeof(EditNoteView) }
                           ,{ typeof(ReservationHistoryViewModel), typeof(ReservationHistoryView) }
                           ,{ typeof(MergeMemberViewModel), typeof(MergeMemberView) }
                        };
        }
    }
}
