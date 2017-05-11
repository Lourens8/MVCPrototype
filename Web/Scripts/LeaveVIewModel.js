// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function LeaveViewModel()
{
    var self = this;

    this.Leaves = ko.observableArray(LeaveList.Leaves);
    this.UserLeaves = ko.observableArray(LeaveList.UserLeaves);
    this.Holidays = ko.observableArray(LeaveList.Holidays);
    this.ShowHistory = ko.observable(false);
    this.ShowFuture = ko.observable(false);
    this.LeaveType = ko.observable('');
    this.UserID = ko.observable('');
    this.History = ko.observable(false);
    this.Future = ko.observable(false);
    this.Manage = ko.observable(false);
    this.FormLeave = ko.observable(DefaultLeave());

    this.FilterMyLeave = function ()
    {
        self.Manage(false);
        self.Future(false);
        self.History(false);
        self.LeaveType('');
    }

    this.FilterManage = function ()
    {
        self.Manage(true);
        self.Future(false);
        self.History(false);
        self.LeaveType('');
    }

    this.FilterHistory = function ()
    {
        self.Manage(false);
        self.Future(false);
        self.History(true);
        self.LeaveType('AnnualLeave');
    }

    this.FilterFuture = function ()
    {
        self.Manage(false);
        self.Future(true);
        self.History(false);
        self.LeaveType('AnnualLeave');
    }

    this.FilterType = function (context, event)
    {
        self.LeaveType(event.target.value);
    }
    
    this.Add = function(typeName)
    {
        self.FormLeave(DefaultLeave());
        self.LeaveType(typeName);
    }

    this.Edit = function (leave)
    {
        self.FormLeave(this);
        $('#DateRange').val(new moment(this.StartDate).format('YYYY-MM-DD') + ' - ' + new moment(this.EndDate).format('YYYY-MM-DD'))
        self.LeaveType(this.TypeName);
    }

    self.LeaveTypes = ko.computed(function ()
    {
        var types = ko.utils.arrayMap(self.Leaves(), function (l)
        {
            return l.TypeName;
        });

        return ko.utils.arrayGetDistinctValues(types).sort();
    });

    self.ToApprove = ko.computed(function ()
    {
        var total = 0;

        ko.utils.arrayForEach(self.UserLeaves(), function (ul)
        {
            ko.utils.arrayForEach(ul.Leaves, function (l)
            {
                if (l.LeaveStateID == 1)
                    total++;
            });
        });

        return total;
    });

    self.CurrentLeave = ko.computed(function ()
    {
        var list;

        //Specific User
        if (self.UserID())
        {
            list = ko.utils.arrayFilter(self.UserLeaves(), function (u)
            {
                return u.UserID == self.UserID();
            })[0].Leaves;
        }
            //All other users
        else
        {
            list = self.Leaves();
        }

        return ko.utils.arrayFilter(list, function (l)
        {
            return l.IsCurrent;
        });
    });

    self.FilteredLeaves = ko.computed(function ()
    {
        var list;

        //Specific User
        if (self.UserID())
        {
            list = ko.utils.arrayFilter(self.UserLeaves(), function (u)
            {
                return u.UserID == self.UserID();
            })[0].Leaves;
        }
        //All other users
        else if (self.Manage())
        {
            list = [];

            ko.utils.arrayForEach(self.UserLeaves(), function (ul)
            {
                ko.utils.arrayForEach(ul.Leaves, function (l)
                {
                    list.push(l);
                });
            });
        }
        //Current User
        else
        {
            list = self.Leaves();
        }

        return ko.utils.arrayFilter(list, function (l)
        {
            var show = true;

            //Show past entries
            show = show && (self.History() == true || new Date(l.StartDate) > new Date() || l.LeaveID > 0 || l.IsCurrent);
            //Show future entries
            show = show && (self.Future() == true || new Date(l.StartDate) < new Date() || l.LeaveID > 0);
            //Filter by leave type
            show = show && (!self.LeaveType() || l.TypeName == self.LeaveType());

            return show;
        });
    });
}

ko.applyBindings(new LeaveViewModel());