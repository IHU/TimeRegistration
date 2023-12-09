export default class TimeEntryViewModel {
    id: number;
    name: string;
    description: string;
    hours: string;
    entryDate: Date;
    isEnd: boolean;
    projectId: number;
    userId: number;

    constructor() {
        this.id= 0;
        this.name = '';
        this.description = '';
        this.hours = '';
        this.entryDate = new Date();
        this.isEnd = false;
        this.projectId = 0;
        this.userId = 0;
    }
}