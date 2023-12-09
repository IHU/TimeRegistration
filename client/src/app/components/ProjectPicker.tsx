import React from 'react'
import ProjectEntryModel from "../models/ProjectViewModel";

export default function ProjectPicker({ projectId, onSelectionChange }) {

    const isDisabled = projectId !== undefined;

    const url = process.env.REACT_APP_BASE_URL + '/api/projects';

    const [data, setData] = React.useState<ProjectEntryModel[]>([]);

    async function getData(): Promise<ProjectEntryModel[]> {
        return fetch(url)
            .then(res => res.json() as Promise<ProjectEntryModel[]>);
    }

    React.useEffect(() => {
        getData().then(item => setData(item))
            .catch((err) => {
                console.log(err.message);
            });

    },
        []);

    const [selected, setSelected] = React.useState(projectId);

    const handleSelectChange = (event) => {
        const selectedValue = event.target.value;
        setSelected(selectedValue);
        onSelectionChange(selectedValue); // Pass selected data to parent
    };
   
    return (
        <select value={selected} onChange={handleSelectChange} disabled={isDisabled}
            className="block appearance-none w-full bg-gray-200 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-state">
            {data?.map(project => (
                <option key={project.id} value={project.id}>{project.name}</option>
            ))}
        </select>
    );
}

