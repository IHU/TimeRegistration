import TimeEntryViewModel from "../models/TimeEntryViewModel";

export async function getAll() {
    const url = process.env.REACT_APP_BASE_URL + `/api/timelog`;
    const response = await fetch(url);
    return response.json();
}
export async function createTimeLog(timeEntry: TimeEntryViewModel): Promise<TimeEntryViewModel> {
    const url = process.env.REACT_APP_BASE_URL + `/api/timelog`;

    try {
        const response = await fetch(`${url}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            mode: 'cors',
            body: JSON.stringify(timeEntry)
        });
        console.log('Time log added successfully:', response);
        return response.json() as Promise<TimeEntryViewModel>;
    } catch (error) {
        console.error('Error adding time log:', error.message);
    }

    return new TimeEntryViewModel();
}

export async function deleteTimeLog(id: number) {
    const url = process.env.REACT_APP_BASE_URL + `/api/timelog/${id}`;
    try {
        const response = await fetch(`${url}`,
            {
                method: 'DELETE',
                mode: 'cors',
            });
        console.log('Time log deleted successfully:', response);
        return response;
    } catch (error) {
        console.error('Error deleting time log:', error.message);
    }
    return null;
}