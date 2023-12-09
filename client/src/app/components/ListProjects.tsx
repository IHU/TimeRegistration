import React from 'react'
import { Link } from 'react-router-dom';
import ProjectEntry from "../models/ProjectViewModel";

export default function ListProjects({ searchQuery }) {
    
    const url = process.env.REACT_APP_BASE_URL + '/api/projects';
    
    const [projects, setProjects] = React.useState<ProjectEntry[]>([]);

    const [sortAsc, setSortAsc] = React.useState(true);

    async function getData(): Promise<ProjectEntry[]> {

        return fetch(url)
            .then(res => res.json() as Promise<ProjectEntry[]>);

    }

    React.useEffect(() => {
        getData().then(item => setProjects(item))
                .catch((err) => {
                    console.log(err.message);
                });
        },
        []);

    const sortByDeadline = () => {
        const sortedProjects = projects.sort(sortAsc ? (a, b) => b.endDate.localeCompare(a.endDate) : (a, b) => a.endDate.localeCompare(b.endDate));
        setSortAsc(!sortAsc);
        setProjects(sortedProjects);
    };

    // Filter projects based on the search term (project ID)
    const filtered = projects.filter((project) =>
        project.id.toString().includes(searchQuery)
    );

    const renderTable = () => {
       
        return filtered?.map(project => {
            return (
                <tr key={project.id} className={project.isCompleted ? 'bg-[#9ca3af]' : 'bg-[#4ade80]'}>
                    <td className="border px-4 py-2 w-12">{project.id}</td>
                    <td className="border px-4 py-2">{project.name}</td>
                    <td className="border px-4 py-2">{project.customerId}</td>
                    <td className="border px-4 py-2">{project.endDate}</td>
                    <td className="border px-4 py-2">{project.hoursSpend}</td>
                    <td className="border px-4 py-2">{project.isCompleted && '✔'}</td>
                    <td className="border px-4 py-2"><Link to={`project/${project.id}`} className="underline text-blue-600 hover:text-blue-800 visited:text-purple-600">View</Link></td>
                </tr>
            );
        });
    }
    return (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">#</th>
                    <th className="border px-4 py-2">Project Name</th>
                    <th className="border px-4 py-2">Customer Name</th>
                    <th className="border px-4 py-2 cursor-pointer" onClick={sortByDeadline}>Deadline</th>
                    <th className="border px-4 py-2">Hours Spend</th>
                    <th className="border px-4 py-2">Status</th>
                    <th className="border px-4 py-2">Action</th>
                </tr>
            </thead>
            <tbody>{renderTable()}</tbody>
        </table>
    );
}

