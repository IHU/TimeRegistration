import ProjectEntryModel from "../models/ProjectViewModel"
export async function getAll() {
    const url = process.env.REACT_APP_BASE_URL + `/api/projects`;
    const response = await fetch(url);
    return response.json();
}
export async function getProjects(id: number): Promise<ProjectEntryModel> {
    const url = process.env.REACT_APP_BASE_URL + `/api/projects/${id}`;
    return await fetch(url)
        .then(res => res.json() as Promise<ProjectEntryModel>);
}