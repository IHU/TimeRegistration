import React from "react";
import { useParams } from "react-router-dom";
import ProjectEntryModel from "../models/ProjectViewModel"
import TimeEntries from "../components/TimeEntries";
import TimeLogEntry from "../components/TimeLogEntry";
import { getProjects } from "../api/projects";


export default function Project() {

    const { id } = useParams();

    const [project, setProject] = React.useState<ProjectEntryModel>([]);

    React.useEffect(() => {
         getProjects(Number(id)).then(p => setProject(p))
            .catch((err) => {
                console.log(err.message);
            });

    }, []);

    const timerEntries = () => {
        return (
            <>
                <div className="flex items-center my-6">
                    <div className="w-1/2">
                        <div className="uppercase tracking-wide text-sm text-indigo-600 font-bold">
                            <h2>Project Id : {project.id}  - {project.name}</h2>
                            <h1>Total Spend Hours : {project.hoursSpend}</h1>
                        </div>
                    </div>
                </div>
                <div className="flex items-center my-6">
                    <div className="w-1/2">
                        <TimeLogEntry />
                    </div>
                </div>
                <table className="table-fixed w-full">
                    <thead className="bg-gray-200">
                        <tr>
                            <th className="border px-4 py-2 w-12">#</th>
                            <th className="border px-4 py-2">Date</th>
                            <th className="border px-4 py-2">Task</th>
                            <th className="border px-4 py-2">Description</th>
                            <th className="border px-4 py-2">Hours Spend</th>
                            <th className="border px-4 py-2">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {project.timeEntries?.map((entry) =>
                            <TimeEntries key={entry.id} timeEntry={entry} />
                        )}
                    </tbody>
                </table>
            </>
        );
    }

    return (
        <div>
            {timerEntries()}
        </div>
    );
}


