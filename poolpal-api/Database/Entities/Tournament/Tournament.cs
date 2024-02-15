using System.Text.RegularExpressions;
using poolpal_api.Models;

namespace poolpal_api.Database.Entities.Tournament
{
    public class Tournament
    {

        public Tournament()
        {
           Status = TournamentStatus.Draft;

        }

        public Tournament(TournamentStatus status)
        {
            Status = status;
        }

        public int TournamentId { get; set; }
        public string Name { get; set; }
        public TournamentFormat Format { get; set; }
        public PoolGameType GameType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamBased { get; set; }
        public TournamentParticipationType ParticipationType { get; set; }
        public TournamentStatus Status { get; private set; }
        public string? Description { get; set; }
        public int? OrganiserId { get; set; }

        // New navigation properties
        public List<Group>? Groups { get; set; } // Groups/Brackets in the tournament
        public List<TournamentRegistration> Registrations { get; set; } // Registrations for the tournament

        // Existing navigation property
        public List<Match>? Matches { get; set; } // Consider if this should be moved under Group

        public Player? Organiser { get; set; }


        private static readonly Dictionary<TournamentStatus, List<TournamentStatus>> AllowedTransitions = new Dictionary<TournamentStatus, List<TournamentStatus>>
        {
            { TournamentStatus.Draft, new List<TournamentStatus> { TournamentStatus.Scheduled, TournamentStatus.Open,TournamentStatus.RegistrationClosed, TournamentStatus.InProgress, TournamentStatus.Cancelled } },
            { TournamentStatus.Scheduled, new List<TournamentStatus> { TournamentStatus.Open, TournamentStatus.Cancelled } },
            { TournamentStatus.Open, new List<TournamentStatus> { TournamentStatus.RegistrationClosed, TournamentStatus.InProgress, TournamentStatus.Cancelled } },
            { TournamentStatus.RegistrationClosed, new List<TournamentStatus> { TournamentStatus.InProgress, TournamentStatus.Cancelled } },
            { TournamentStatus.InProgress, new List<TournamentStatus> { TournamentStatus.Paused, TournamentStatus.Finished, TournamentStatus.Cancelled } },
            { TournamentStatus.Paused, new List<TournamentStatus> { TournamentStatus.Resumed, TournamentStatus.Cancelled } },
            { TournamentStatus.Resumed, new List<TournamentStatus> { TournamentStatus.InProgress, TournamentStatus.Finished, TournamentStatus.Cancelled } },
            // Finished and Cancelled states have no further transitions
        };

        public bool TryChangeStatus(TournamentStatus newStatus)
        {
            if (!AllowedTransitions[Status].Contains(newStatus)) return false;
            Status = newStatus;
            return true;
        }
    }



    public enum TournamentParticipationType
    {
        Open,
        InviteOnly,
    }

    public enum TournamentStatus
    {
        Draft = 1,        // Tournament is being set up
        Scheduled = 2,    // Tournament dates and details are finalized, but registration is not open yet
        Open = 3,         // Registration is open
        RegistrationClosed = 4, // Registration is closed, but the tournament hasn't started
        InProgress = 5,   // Tournament is in progress 
        Paused = 6,       // Tournament is temporarily paused (e.g., due to unforeseen circumstances)
        Resumed = 7,      // Tournament has resumed after being paused
        Finished = 8,     // Tournament has concluded
        Cancelled = 9     // Tournament has been cancelled
    }
}