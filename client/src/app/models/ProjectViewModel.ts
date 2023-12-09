import TimeEntryModel from "./TimeEntryViewModel"
export default class ProjectViewModel {
    id: string;
    name: string;
    customerId: string;
    notes: string;
    userId: string;
    endDate: string;
    isCompleted: boolean;
    hoursSpend: number;
    timeEntries: TimeEntryModel[];

    constructor() {
        this.id= '';
        this.name = '';
        this.customerId = '';
        this.notes = '';
        this.userId = '';
        this.endDate = '';
        this.isCompleted = false;
        this.hoursSpend = 0;
        this.timeEntries = [];
    }
}