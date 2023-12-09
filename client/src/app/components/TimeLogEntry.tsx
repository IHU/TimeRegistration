import React, { useState } from "react";
import { useParams } from "react-router-dom";

import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

import ProjectPicker from "./ProjectPicker";
import TimeEntryViewModel from "../models/TimeEntryViewModel";
import { createTimeLog } from "../api/timelog";

export default function TimeLogEntry() {

    const { id } = useParams();

    const [showModal, setShowModal] = useState(false);
    const [taskName, setTaskName] = useState('');
    const [description, setDescription] = useState('');
    const [hoursWorked, setHoursWorked] = useState('');

    const [startDate, setStartDate] = useState(new Date());
    const [selectedProjectId, setSelectedProjectId] = useState(id);
    const [isValid, setIsValid] = useState(true);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const timeEntry = new TimeEntryViewModel();
        timeEntry.hours = hoursWorked;
        timeEntry.name = taskName;
        timeEntry.description = description;
        timeEntry.projectId = Number(selectedProjectId);
        timeEntry.entryDate = startDate;
        timeEntry.userId = 1;

        if (isValid) {
            createTimeLog(timeEntry);
            setShowModal(false);
            window.location.reload();
        }
        else {
            console.error('Invalid input.');
        }
    };


    const handleSelect = (selectedValue) => {
        setSelectedProjectId(selectedValue);
    };

    const handleInputChange = (e) => {
        const value = e.target.value;
        
        const workedHoursRegex = /^(?:\d+(\.\d+)?|0(\.\d*[3-9])?|[1-9]\d*(\.\d+)?)$/;
        const isValidInput = workedHoursRegex.test(value);
        console.log(value + "-" + isValidInput);
        setIsValid(value === '' || isValidInput);
        setHoursWorked(value);
    };
    return (
        <>
            <button type="button" onClick={() => setShowModal(true)} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Log Hour(s)</button>
            {showModal ? (
                <>
                    <form onSubmit={handleSubmit}>
                        <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                            <div className="relative w-auto my-6 mx-auto max-w-sm">
                                {/*content*/}
                                <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                                    {/*header*/}
                                    <div className="flex items-start justify-between p-5 border-b border-solid border-blueGray-200 rounded-t">
                                        <h3 className="text-3xl font-semibold">
                                            Register Time 
                                        </h3>
                                        <button className="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                                            onClick={() => setShowModal(false)}>
                                            <span className="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">
                                                ×
                                            </span>
                                        </button>
                                    </div>
                                    {/*body*/}
                                    <div className="relative p-6 flex-auto">
                                        <div className="flex flex-wrap -mx-3 mb-6">

                                            <div className="w-full px-3">
                                                <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
                                                    Project
                                                </label>
                                                <ProjectPicker key={id} projectId={id} onSelectionChange={handleSelect} />
                                            </div>
                                        </div>
                                        <div className="flex flex-wrap -mx-3 mb-6">
                                            <div className="w-full px-3">
                                                <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
                                                    Task
                                                </label>
                                                <input className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-task" type="text"
                                                    value={taskName}
                                                    onChange={(e) => setTaskName(e.target.value)}
                                                    required={true}
                                                />
                                            </div>
                                        </div>
                                        <div className="flex flex-wrap -mx-3 mb-6">
                                            <div className="w-full px-3">
                                                <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
                                                    Description
                                                </label>
                                                <textarea className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-description"
                                                    value={description}
                                                    onChange={(e) => setDescription(e.target.value)} />
                                            </div>

                                        </div>
                                        <div className="flex flex-wrap -mx-3 mb-6">
                                            <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                                                <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
                                                    Date
                                                </label>
                                                <DatePicker selected={startDate} onChange={(date) => setStartDate(date)} className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" />
                                            </div>
                                            <div className="w-full md:w-1/2 px-3">
                                                <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
                                                    Hour(s) 
                                                </label>
                                                <input className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-worked-hours" type="text"
                                                    value={hoursWorked}
                                                    onChange={handleInputChange}
                                                    required={true}
                                                    style={{ borderColor: isValid ? '' : 'red' }}
                                                />
                                            </div>
                                                <p>Format hh.mm : i.e. 00.30 or 5.30 or 01.30 or 05.30 or 0.30 or 0.45</p>
                                        </div>
                                    </div>
                                    {/*footer*/}
                                    <div className="flex items-center justify-end p-6 border-t border-solid border-blueGray-200 rounded-b">
                                        <button className="text-red-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                                            type="button"
                                            onClick={() => setShowModal(false)}>
                                            Close
                                        </button>
                                        <button className="bg-emerald-500 text-white active:bg-emerald-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                                            type="submit">
                                            Save Changes
                                        </button>
                                    </div>
                                    <div className="flex items-center justify-end p-6 border-t border-solid border-blueGray-200 rounded-b"> 
                                        {!isValid && (
                                            <p style={{ color: 'red' }}>
                                                Please enter a valid time registrations. Time registrations should be 30 minutes or longer.
                                            </p>
                                        )}</div>
                                </div>
                            </div>
                        </div>
                        <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
                    </form>
                   
                </>
            ) : null}
        </>
    );
}

