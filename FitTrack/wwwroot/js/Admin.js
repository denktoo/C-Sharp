function AdminViewModel() {
    var self = this;

    self.totalUsers = ko.observable(0);
    self.totalWorkouts = ko.observable(0);
    self.goalsAchieved = ko.observable(0);
    self.pendingGoals = ko.observable(0);

    // Observable array to store recent goals
    self.recentGoals = ko.observableArray([]);

    // Observable array to store recent goals
    self.users = ko.observableArray([]);

    // Observable array to store recent goals
    self.recentWorkouts = ko.observableArray([]);

    self.user = ko.observable(null);

    // Function to fetch from the server
    self.fetchData = function () {
        fetch('/api/AdminApi/Dashboard')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {

                // Update Statistics
                self.totalUsers(data.totalUsers);
                self.totalWorkouts(data.totalWorkouts);
                self.goalsAchieved(data.goalsAchieved);
                self.pendingGoals(data.pendingGoals);

                // Update recentGoals
                if (data.recentGoals) {
                    // Map the data to the observable array
                    self.recentGoals(data.recentGoals.map(goal => ({
                        userName: goal.userName || 'Unknown User',
                        description: goal.description,
                        targetDate: new Date(goal.targetDate).toLocaleDateString()
                    })));
                }

                // Update users
                if (data.users) {
                    // Map the data to the observable array
                    self.users(data.users.filter(user => user.role !== "Admin").map(user => ({
                        id: user.id,
                        name: user.name,
                        email: user.email,
                        workoutsCount: user.workoutsCount,
                        goalsAchievedCount: user.goalsAchievedCount
                    })));
                }

                // Update recentWorkouts
                if (data.recentWorkouts) {
                    self.recentWorkouts(data.recentWorkouts.map(workout => ({
                        userName: workout.userName,
                        date: new Date(workout.date).toLocaleDateString(),
                        duration: workout.duration
                    })));
                }
            })
            .catch(error => console.error('Error fetching recent goals:', error));
    };

    // fetch user details
    self.fetchUserDetails = function (userId) {
        fetch('/api/AdminApi/User/' + userId)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {

                self.user({
                    name: data.user.name,
                    email: data.user.email,
                    role: data.user.role,
                    workoutsCount: data.user.workoutsCount,
                    goalsAchievedCount: data.user.goalsAchievedCount
                });

                // Update recentGoals
                if (data.recentGoals) {
                    // Map the data to the observable array
                    self.recentGoals(data.recentGoals.map(goal => ({
                        description: goal.description,
                        targetDate: new Date(goal.targetDate).toLocaleDateString()
                    })));
                }

                // Update recentWorkouts
                if (data.recentWorkouts) {
                    self.recentWorkouts(data.recentWorkouts.map(workout => ({
                        date: new Date(workout.date).toLocaleDateString(),
                        duration: workout.duration
                    })));
                }
                // Redirect to ViewUser page
                window.location.href = '/Admin/ViewUser';
            })
            .catch(error => console.error('Error fetching user details:', error));
    };

    // Initial fetch
    self.fetchData();

    // Initial fetch for user details
    self.fetchUserDetails();
}

// Apply the Knockout.js bindings
ko.applyBindings(new AdminViewModel());
