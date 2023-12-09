import React from 'react'
import { deleteTimeLog } from "../api/timelog";

export default function TimeEntries(props) {

    const handleDelete = async (e) => {
        e.preventDefault();
        deleteTimeLog(props.timeEntry.id);
        window.location.reload();
    };
    const handleUpdate = async (e) => {
        e.preventDefault();
        alert("not implemented!");
    };
    return (
        <tr key={props.timeEntry.id}>
            <td className="border px-4 py-2 w-12">{props.timeEntry.id}</td>
            <td className="border px-4 py-2">{new Date(props.timeEntry.entryDate).toLocaleDateString()}</td>
            <td className="border px-4 py-2">{props.timeEntry.name}</td>
            <td className="border px-4 py-2">{props.timeEntry.description}</td>
            <td className="border px-4 py-2">{props.timeEntry.hours}</td>
            <td className="border px-4 py-2">
                <button className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2" type="submit" onClick={handleUpdate}>Update</button> / 
                <button className="bg-red-500 hover:bg-red-700 text-white rounded-full py-2 px-4 ml-2" type="submit" onClick={handleDelete}>Delete</button>
                
            </td>
        </tr>
    );
}
